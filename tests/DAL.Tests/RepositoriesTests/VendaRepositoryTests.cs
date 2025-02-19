using FakeItEasy;
using FluentAssertions;
using MapsterMapper;
using Omie.Application;
using Omie.Application.Models;
using Omie.Application.Services;
using Omie.DAL;
using Omie.Domain.Entities;
using Tests.Common.Fixtures;

namespace DAL.Tests;

public class VendaRepositoryTests : IClassFixture<DatabaseFixture<Venda, DbContextOmie>>
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IMapper _fakeMapper;
    private readonly IVendaAppService _vendaAppService;
    private readonly DatabaseFixture<Venda, DbContextOmie> _fixture;

    public VendaRepositoryTests()
    {
        // Create an in-memory DbContext
        _fixture = new DatabaseFixture<Venda, DbContextOmie>();
        _vendaRepository = new VendaRepository(_fixture.Context);
        _fakeMapper = A.Fake<IMapper>();
        _vendaAppService = new VendaAppService(_vendaRepository, _fakeMapper); // Initialize with appropriate parameters if needed
    }

    [Fact]
    public async Task AddAsync_ShouldAddVendaSuccessfully()
    {
        var vendas = _fixture.GetEntities<Venda>(1, true);
        foreach (var venda in vendas)
        {
            venda.CodigoVenda = _vendaAppService.GenerateCodigoVenda(new VendaInsertingDto(){Cliente = venda.Cliente});
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.SaveChangesAsync();

            var addedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            addedVenda.Should().NotBeNull();
            addedVenda?.Id.Should().Be(venda.Id);
            addedVenda?.CodigoVenda.Should().Be(venda.CodigoVenda);
            addedVenda?.Cliente.Should().Be(venda.Cliente);
            addedVenda?.DataDaVenda.Should().Be(venda.DataDaVenda);
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnVendaById()
    {
        var vendas = _fixture.GetEntities<Venda>(1, true);
        foreach (var venda in vendas)
        {
            venda.CodigoVenda = _vendaAppService.GenerateCodigoVenda(new VendaInsertingDto(){Cliente = venda.Cliente});
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.SaveChangesAsync();

            var addedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            addedVenda.Should().NotBeNull();
            addedVenda?.Id.Should().Be(venda.Id);
            addedVenda?.CodigoVenda.Should().Be(venda.CodigoVenda);
            addedVenda?.Cliente.Should().Be(venda.Cliente);
            addedVenda?.DataDaVenda.Should().Be(venda.DataDaVenda);
        }
    }

    [Fact]
    public async Task Update_ShouldUpdateVendaSuccessfully()
    {
        var vendas = _fixture.GetEntities<Venda>(3, true);
        foreach(var venda in vendas)
        {
            venda.CodigoVenda = _vendaAppService.GenerateCodigoVenda(new VendaInsertingDto(){Cliente = venda.Cliente});
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.SaveChangesAsync();

            var updatedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            updatedVenda.DataDaVenda = DateTime.Now;
            _vendaRepository.Update(updatedVenda);
            await _vendaRepository.SaveChangesAsync();

            var retrievedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            retrievedVenda.Should().NotBeNull();
            retrievedVenda?.Id.Should().Be(updatedVenda.Id);
            retrievedVenda?.CodigoVenda.Should().Be(updatedVenda.CodigoVenda);
            retrievedVenda?.Cliente.Should().Be(updatedVenda.Cliente);
            retrievedVenda?.DataDaVenda.Should().Be(updatedVenda.DataDaVenda);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteVendaSuccessfully()
    {
        var vendas = _fixture.GetEntities<Venda>(3, true);
        foreach (var venda in vendas)
        {
            venda.CodigoVenda = _vendaAppService.GenerateCodigoVenda(new VendaInsertingDto(){Cliente = venda.Cliente});
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.SaveChangesAsync();

            await _vendaRepository.DeleteAsync(venda.Id);
            await _vendaRepository.SaveChangesAsync();

            var deletedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            deletedVenda.Should().NotBeNull();
            deletedVenda?.DeletedAt.Should().NotBeNull();
            deletedVenda?.DeletedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(1000));

            var allVendas = await _vendaRepository.GetAllAsync();
            var notFoundVenda = allVendas.FirstOrDefault(c => c.Id == venda.Id);
            notFoundVenda.Should().BeNull();
        }
    }
}