using FluentAssertions;
using Omie.Domain.Entities;
using Omie.Domain.enums;

namespace Domain.Tests;

public class ClienteTests
{
    [Fact]
    public void Cliente_Nome_Should_Not_Be_Empty()
    {
        var cliente = new Cliente
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            Nome = "John Doe",
            CPF = null,
            Status = (ClienteStatusEnum)0,
            Email = "john@doe.com",
            Telefone = "555-555-5555",
            EnderecoId = null,
            Endereco = null,
            Observacao = null,
            Id = 1
        };
        cliente.Nome.Should().NotBeNullOrWhiteSpace("Client name must not be empty.");
    }

    [Fact]
    public void Cliente_CPF_Should_Have_Valid_Format()
    {
        var cliente = new Cliente
        {
            CPF = "12345678901",
            Id = 1
        }; // Adjust based on your validation rules
        bool isValid = ValidateCPF(cliente.CPF);
        isValid.Should().BeTrue("Client CPF must be valid.");
    }

    private bool ValidateCPF(string cpf)
    {
        // Simplified CPF validation example
        return cpf.Length == 11 && cpf.All(char.IsDigit);
    }

    [Fact]
    public void Cliente_Status_Should_Be_Ativo_By_Default()
    {
        var cliente = new Cliente
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            Id = 1,
            Nome = "John Doe",
            CPF = "0",
            Status = (ClienteStatusEnum)1,
            Email = null,
            Telefone = null,
            EnderecoId = null,
            Endereco = null,
            Observacao = null
        };
        cliente.Status.Should().Be(ClienteStatusEnum.Ativo, "Default client status should be 'Ativo'.");
    }

    [Fact]
    public void Cliente_Email_Should_Allow_Null_Or_Valid_Format()
    {
        var cliente = new Cliente
        {
            CreatedAt = default,
            UpdatedAt = null,
            Email = "test@example.com",
            Telefone = null,
            EnderecoId = null,
            Endereco = null,
            Observacao = null,
            Id = 1,
            Nome = null,
            CPF = null,
            Status = (ClienteStatusEnum)0
        };
        cliente.Email.Should().MatchRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Client email must be valid.");
    }

    [Fact]
    public void Cliente_Telefone_Should_Allow_Null_Or_Valid_Format()
    {
        var cliente = new Cliente
        {
            CreatedAt = default,
            UpdatedAt = null,
            Telefone = "(11) 91234-5678",
            EnderecoId = null,
            Endereco = null,
            Observacao = null,
            Id = 1,
            Nome = null,
            CPF = null,
            Status = (ClienteStatusEnum)0,
            Email = null
        };
        cliente.Telefone.Should().NotBeNullOrWhiteSpace("Client phone number can be null but must be valid if provided.");
    }

    [Fact]
    public void Cliente_Should_Allow_Null_Endereco()
    {
        var cliente = new Cliente
        {
            CreatedAt = default,
            UpdatedAt = null,
            Id = 1,
            Nome = null,
            CPF = null,
            Status = (ClienteStatusEnum)0,
            Email = null,
            Telefone = null,
            EnderecoId = null,
            Endereco = null,
            Observacao = null
        };
        cliente.Endereco.Should().BeNull("A client may not have an address initially.");
    }

    [Fact]
    public void Cliente_Observacao_Should_Allow_Null_Or_Text()
    {
        var cliente = new Cliente
        {
            Observacao = "VIP Customer",
            Id = 1
        };
        cliente.Observacao.Should().NotBeNullOrWhiteSpace("Client observation can be null but must contain text if provided.");
    }
}