using AutoFixture;
using FluentAssertions;
using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain.Entities;
using Omie.WebApi;
using Tests.Common.Data;
using Tests.Common.Fixtures;
using Xunit;

namespace Database.Tests;
public class VendaTests : IClassFixture<DatabaseFixture<Venda, DbContextOmie>>
{
    private readonly DatabaseFixture<Venda, DbContextOmie> _fixture;
    private readonly IVendaRepository _vendaRepository;
    private readonly ClienteRepository _clienteRepository;

    public VendaTests(DatabaseFixture<Venda, DbContextOmie> fixture)
    {
        _fixture = fixture;
        _fixture.SetWorkWithSqlServer();
        _vendaRepository = new VendaRepository((DbContextOmie)_fixture.Context);
        _clienteRepository = new ClienteRepository((DbContextOmie)_fixture.Context);
    }

    [Fact]
    public async Task Test_CreateVenda()
    {
        for(int i = 0; i < 12; i++)
        {
            var venda = await AddOneVenda();
            var retrievedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
            retrievedVenda.Should().NotBeNull();
            retrievedVenda.CodigoVenda.Should().Be(venda.CodigoVenda);
            retrievedVenda.Id.Should().Be(venda.Id);
            retrievedVenda.Cliente.Should().Be(venda.Cliente);
            retrievedVenda.DataDaVenda.Should().Be(venda.DataDaVenda);
        }
    }

    private async Task<Venda> AddOneVenda()
    {
        var vendas = TestData.GetVendas(_fixture, 1);
        var cliente = new Fixture().Create<string>();
        foreach (var venda in vendas)
        {
            venda.Cliente = null;
            venda.Cliente = cliente;
            venda.CodigoVenda = venda.CodigoVenda.Substring(0, 12);
            venda.DataDaVenda = DateTime.Now;
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.AddAsync(venda);
            await _vendaRepository.SaveChangesAsync();
        }
        
        return _vendaRepository.GetAllAsync().Result.FirstOrDefault();
    }

    [Fact]
    public async Task Test_ReadVenda()
    {
        var venda = await AddOneVenda();
        var allVendas = await _vendaRepository.GetAllAsync();
        allVendas.Should().NotBeNull();
        allVendas.Count().Should().BeGreaterThan(0);
        allVendas.Should().Contain(v => v.Id == venda.Id);
    }

    [Fact]
    public async Task Test_DeleteVenda()
    {
        var venda = await AddOneVenda();
        await _vendaRepository.DeleteAsync(venda.Id);
        await _vendaRepository.SaveChangesAsync();
        var deletedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
        deletedVenda.Should().BeNull();
    }

    [Fact]
    public async Task Test_UpdateVenda()
    {
        var venda = await AddOneVenda();
        var newDate = DateTime.Now.AddDays(1);
        venda.DataDaVenda = newDate;
        await _vendaRepository.SaveChangesAsync(); // Assuming Update method is implicitly called by SaveChanges for tracked entities
        var updatedVenda = await _vendaRepository.GetByIdAsync(venda.Id);
        updatedVenda.DataDaVenda.Should().Be(newDate);
    }

    private async Task<Cliente> AddOneClient()
    {
        var cliente = TestData.GetCliente(new DatabaseFixture<Cliente, DbContextOmie>());
        cliente.CPF = cliente.CPF.Substring(0,11);
        cliente.Email = $"{cliente.Email.Substring(0,10)}@teste{cliente.Email.Substring(10,4)}.com";
        cliente.Telefone = cliente.Telefone.Substring(0,11);
        cliente.Endereco.CEP = cliente.Endereco.CEP.Substring(0,8);
        cliente.Endereco.Numero = cliente.Endereco.Numero.Substring(0,2);
        cliente.Endereco.Complemento = cliente.Endereco.Complemento.Substring(0,15);
        cliente.Endereco.Bairro = cliente.Endereco.Bairro.Substring(0,10);
        cliente.Endereco.Cidade = cliente.Endereco.Cidade.Substring(0,10);
        cliente.Endereco.Estado = cliente.Endereco.Estado.Substring(0,2);
        await _clienteRepository.AddAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
        return cliente;
    }
}