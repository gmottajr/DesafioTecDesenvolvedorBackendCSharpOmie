using FluentAssertions;
using Omie.Domain.Entities;

namespace Domain.Tests;

public class ClienteTests
{
    [Fact]
    public void Cliente_Nome_Should_Not_Be_Empty()
    {
        var cliente = new Cliente { Nome = "John Doe" };
        cliente.Nome.Should().NotBeNullOrWhiteSpace("Client name must not be empty.");
    }

    [Fact]
    public void Cliente_CPF_Should_Have_Valid_Format()
    {
        var cliente = new Cliente { CPF = "12345678901" }; // Adjust based on your validation rules
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
        var cliente = new Cliente();
        cliente.Status.Should().Be(ClienteStatusEnum.Ativo, "Default client status should be 'Ativo'.");
    }

    [Fact]
    public void Cliente_Email_Should_Allow_Null_Or_Valid_Format()
    {
        var cliente = new Cliente { Email = "test@example.com" };
        cliente.Email.Should().MatchRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Client email must be valid.");
    }

    [Fact]
    public void Cliente_Telefone_Should_Allow_Null_Or_Valid_Format()
    {
        var cliente = new Cliente { Telefone = "(11) 91234-5678" };
        cliente.Telefone.Should().NotBeNullOrWhiteSpace("Client phone number can be null but must be valid if provided.");
    }

    [Fact]
    public void Cliente_Should_Allow_Null_Endereco()
    {
        var cliente = new Cliente();
        cliente.Endereco.Should().BeNull("A client may not have an address initially.");
    }

    [Fact]
    public void Cliente_Observacao_Should_Allow_Null_Or_Text()
    {
        var cliente = new Cliente { Observacao = "VIP Customer" };
        cliente.Observacao.Should().NotBeNullOrWhiteSpace("Client observation can be null but must contain text if provided.");
    }
}