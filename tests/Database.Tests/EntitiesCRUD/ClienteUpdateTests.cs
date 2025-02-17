using FluentAssertions;
using Omie.DAL;
using Omie.Domain;
using Omie.WebApi;
using Tests.Common.Data;
using Tests.Common.Fixtures;

namespace Database.Tests;

public class ClienteUpdateTests : IClassFixture<DatabaseFixture<DbContextOmie, VendaController>>
{
    private readonly DatabaseFixture<DbContextOmie, VendaController> _fixture;
    private readonly IClienteRepository _clienteRepository;
    public ClienteUpdateTests(DatabaseFixture<DbContextOmie, VendaController> fixture)
    {
        _fixture = fixture;
        _clienteRepository = new ClienteRepository(fixture.Context);
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
}
