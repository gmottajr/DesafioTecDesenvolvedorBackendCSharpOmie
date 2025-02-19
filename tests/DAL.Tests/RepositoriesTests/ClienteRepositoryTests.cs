using FluentAssertions;
using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain.Entities;
using Tests.Common.Fixtures;

namespace DAL.Tests;

public class ClienteRepositoryTests : IClassFixture<DatabaseFixture<Cliente, DbContextOmie>>
{
    private readonly ClienteRepository _clienteRepository;
    private readonly DatabaseFixture<Cliente, DbContextOmie> _fixture;

    public ClienteRepositoryTests(DatabaseFixture<Cliente, DbContextOmie> fixture)
    {
        _fixture = fixture;
        _clienteRepository = new ClienteRepository(_fixture.Context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddClienteSuccessfully()
    {
        var clientes = _fixture.GetEntities<Cliente>(3, true);
        foreach (var cliente in clientes)
        {
            var clie = await AddOneClient(cliente);
            var addedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            Assert.NotNull(addedCliente);
            Assert.Equal(clie.Id, addedCliente?.Id);
        }
        
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnClienteById()
    {
        var clientes = _fixture.GetEntities<Cliente>(3, true);
        foreach (var cliente in clientes)
        {
            var clie = await AddOneClient(cliente);
            var addedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            addedCliente.Should().NotBeNull();
            clie.Id.Should().Be(addedCliente?.Id);
        }
    }

    [Fact]
    public async Task Update_ShouldUpdateClienteSuccessfully()
    {
        var clientes = _fixture.GetEntities<Cliente>(3, true);

        foreach (var cliente in clientes)
        {
            var clie = await AddOneClient(cliente);
            var addedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            addedCliente.Should().NotBeNull();
            clie.Id.Should().Be(addedCliente?.Id);

            addedCliente.Nome = "Nome Alterado";
            _clienteRepository.Update(addedCliente);
            await _clienteRepository.SaveChangesAsync();

            var updatedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            updatedCliente.Should().NotBeNull();
            updatedCliente?.Nome.Should().Be("Nome Alterado");
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteClienteSuccessfully()
    {
        var clientes = _fixture.GetEntities<Cliente>(3, true);
        foreach (var cliente in clientes)
        {
            var clie = await AddOneClient(cliente);
            var addedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            addedCliente.Should().NotBeNull();
            clie.Id.Should().Be(addedCliente?.Id);

            await _clienteRepository.DeleteAsync(clie.Id);
            await _clienteRepository.SaveChangesAsync();

            var deletedCliente = await _clienteRepository.GetByIdAsync(clie.Id);
            deletedCliente.Should().BeNull();
        }
        
    }
    
    private async Task<Cliente> AddOneClient(Cliente cliente)
    {
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

