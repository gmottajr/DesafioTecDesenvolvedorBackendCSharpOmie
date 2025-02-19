using FluentAssertions;
using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain.Entities;
using Omie.WebApi;
using Tests.Common.Data;
using Tests.Common.Fixtures;

namespace Database.Tests;

public class ClienteTests : IClassFixture<DatabaseFixture<Cliente, DbContextOmie>>
{
    private readonly DatabaseFixture<Cliente, DbContextOmie> _fixture;
    private readonly IClienteRepository _clienteRepository;
    public ClienteTests(DatabaseFixture<Cliente, DbContextOmie> fixture) 
    {
        _fixture = fixture;
        _fixture.SetWorkWithSqlServer();
        _clienteRepository = new ClienteRepository(_fixture.Context);
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

    [Fact]
    public async Task AddinManyClienteShouldBehaveAsExpectedAndPersistAllRecordsSuccessfully()
    {
        for(int i = 0; i < 5; i++)
        {
            var cliente = await AddOneClient();
            var retrievedCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
            retrievedCliente.Should().NotBeNull();
            retrievedCliente.Nome.Should().Be(cliente.Nome);
            retrievedCliente.Id.Should().Be(cliente.Id);
            retrievedCliente.CPF.Should().Be(cliente.CPF);
            retrievedCliente.Email.Should().Be(cliente.Email);
        }
    }

    private async Task<Cliente> AddOneClient()
    {
        var cliente = TestData.GetCliente(_fixture);
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

    [Fact]
    public async Task Test_ReadCliente()
    {
        var cliente = await AddOneClient();
        for(int i = 0; i < 5; i++)
        {
            await AddOneClient();
        }
        var allClientes = await _clienteRepository.GetAllAsync();
        allClientes.Should().NotBeNull();
        allClientes.Count().Should().BeGreaterThan(0);
        allClientes.Should().Contain(c => c.Id == cliente.Id);
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

    [Fact]
    public async Task Test_UpdateCliente()
    {
        var cliente = await AddOneClient();
        var newName = "Updated Name";
        cliente.Nome = newName;
        //_clienteRepository.Update(cliente);
        await _clienteRepository.SaveChangesAsync();
        var updatedCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
        updatedCliente.Nome.Should().Be(newName);
    }
}