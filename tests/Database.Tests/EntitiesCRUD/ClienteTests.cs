using FluentAssertions;
using Omie.DAL;
using Omie.Domain;
using Omie.WebApi;
using Tests.Common.Data;
using Tests.Common.Fixtures;

namespace Database.Tests;

public class ClienteTests : IClassFixture<DatabaseFixture<DbContextOmie, VendaController>>
{
    private readonly DatabaseFixture<DbContextOmie, VendaController> _fixture;
    private readonly IClienteRepository _clienteRepository;
    public ClienteTests(DatabaseFixture<DbContextOmie, VendaController> fixture)
    {
        _fixture = fixture;
        _clienteRepository = new ClienteRepository(fixture.Context);
    }

    [Fact]
    public async Task Test_CreateCliente()
    {
        var cliente = await AddOneClient();
        var retrievedCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
        retrievedCliente.Should().NotBeNull();
        retrievedCliente.Nome.Should().Be(cliente.Nome);
        retrievedCliente.Id.Should().Be(cliente.Id);
        retrievedCliente.CPF.Should().Be(cliente.CPF);
        retrievedCliente.Email.Should().Be(cliente.Email);
    }

    private async Task<Cliente> AddOneClient()
    {
        var cliente = TestData.GetCliente(_fixture);

        await _clienteRepository.AddAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
        return cliente;
    }

    [Fact]
    public async Task Test_ReadCliente()
    {
        var cliente = AddOneClient();
        var allClientes = await _clienteRepository.GetAllAsync();
        allClientes.Should().NotBeNull();
        allClientes.Count().Should().BeGreaterThan(0);
        allClientes.Should().Contain(c => c.Id == cliente.Id);
    }

    [Fact]
    public async Task Test_UpdateCliente()
    {
        var cliente = await AddOneClient();
        var newName = "Updated Name";
        cliente.Nome = newName;
        _clienteRepository.Update(cliente);
        await _clienteRepository.SaveChangesAsync();
        var updatedCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
        updatedCliente.Nome.Should().Be(newName);
    }
    
    [Fact]
    public async Task Test_DeleteCliente()
    {
        var cliente = await AddOneClient();
        await _clienteRepository.DeleteAsync(cliente.Id);
        await _clienteRepository.SaveChangesAsync();
        var deletedCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
        deletedCliente.Should().BeNull();
    }
}