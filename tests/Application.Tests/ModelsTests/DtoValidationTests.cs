using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Omie.Application.Models;
using Omie.Application.Models.Abstractions;


namespace Application.Tests.Models;


public class DtoValidationTests
{
    private static ValidationContext GetValidationContext(object obj) => new(obj, null, null);

    private static bool ValidateModel(object model, out ValidationResult[] results)
    {
        var context = GetValidationContext(model);
        var validationResults = new System.Collections.Generic.List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, validationResults, true);
        results = validationResults.ToArray();
        return isValid;
    }

    [Theory]
    [InlineData(typeof(ClienteInsertingDto))]
    [InlineData(typeof(ItemDto))]
    [InlineData(typeof(ItemInsertingDto))]
    [InlineData(typeof(ProdutoInsertingDto))]
    [InlineData(typeof(VendaInsertingDto))]
    public void Dto_Validation_Should_Fail_When_Required_Fields_Are_Empty(Type dtoType)
    {
        var instance = Activator.CreateInstance(dtoType);
        var isValid = ValidateModel(instance, out var results);
        isValid.Should().BeFalse();
        results.Should().NotBeEmpty();
    }

    [Fact]
    public void ClienteInsertingDto_Should_Be_Valid_When_Filled_Correctly()
    {
        var cliente = A.Fake<ClienteInsertingDto>();
        cliente.Nome = "João Silva";
        cliente.CPF = "12345678901";
        cliente.Status = 2;
        cliente.Email = "joao@email.com";
        cliente.Telefone = "11987654321";

        var isValid = ValidateModel(cliente, out var results);
        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [Fact]
    public void VendaInsertingDto_Should_Be_Valid_When_Filled_Correctly()
    {
        var venda = A.Fake<VendaInsertingDto>();
        venda.DataDaVenda = DateTime.UtcNow;
        venda.ClienteId = 1;
        venda.Itens = new List<ItemInsertingDto>
        {
            new ItemInsertingDto
            {
                VendaId = 1,
                ProdutoId = 10,
                Quantidade = 2
            }
        };

        var isValid = ValidateModel(venda, out var results);
        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }
    
    [Fact]
    public void ProdutoInsertingDto_Should_Be_Valid_When_Filled_Correctly()
    {
        var produto = A.Fake<ProdutoInsertingDto>();
        produto.Nome = "Notebook Dell";
        produto.Descricao = "Notebook com processador i7, 16GB RAM e SSD 512GB";
        produto.Valor = 4999.99m;
        produto.Categoria = "Informática";
        produto.Imagem = "https://example.com/produto.jpg";
        produto.Marca = "Dell";
        produto.Unidade = "UN1";
        produto.Tipo = "Eletrônico";
        produto.Codigo = "ABCDE1234567";

        var isValid = ValidateModel(produto, out var results);
        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }
    
    [Fact]
    public void ProdutoInsertingDto_Should_Be_Invalid_When_Fields_Are_Empty()
    {
        var produto = new ProdutoInsertingDto
        {
            Nome = "",
            Descricao = "",
            Valor = 0, // Invalid: Must be greater than zero
            Categoria = "",
            Imagem = "invalid_url", // Invalid: Not a valid URL
            Marca = "",
            Unidade = "1A", // Invalid: Doesn't match required pattern
            Tipo = "",
            Codigo = "12345ABCDE" // Invalid: Doesn't match required pattern
        };

        var isValid = ValidateModel(produto, out var results);
    
        isValid.Should().BeFalse();
        results.Should().NotBeEmpty();
        results.Should().Contain(r => r.ErrorMessage.Contains("The Nome field is required."));
        results.Should().Contain(r => r.ErrorMessage.Contains("The Descrição field is required."));
        results.Should().Contain(r => r.ErrorMessage.Contains("Valor do produto deve ser maior que zero."));
        results.Should().Contain(r => r.ErrorMessage.Contains("The Categoria field is required."));
        results.Should().Contain(r => r.ErrorMessage.Contains("O valor do campo imagem precisa ser uma URL válida."));
        results.Should().Contain(r => r.ErrorMessage.Contains("The Marca do Produto field is required."));
        results.Should().Contain(r => r.ErrorMessage.Contains("Unidade deve começar com letras e pode ter até 3 números no final."));
        results.Should().Contain(r => r.ErrorMessage.Contains("The Tipo field is required."));
        results.Should().Contain(r => r.ErrorMessage.Contains("Código deve começar com 5 letras maiúsculas seguidas de 7 dígitos numéricos."));
    }
    
    [Fact]
    public void VendaInsertingDto_Should_Be_Invalid_When_Missing_Required_Fields()
    {
        var venda = new VendaInsertingDto
        {
            DataDaVenda = default, // Invalid: DateTime default value
            ClienteId = 0, // Invalid: Must be greater than zero
            Itens = null // Invalid: Required field
        };

        var isValid = ValidateModel(venda, out var results);
    
        isValid.Should().BeFalse();
        results.Should().NotBeEmpty();
        results.Should().Contain(r => r.ErrorMessage.Contains("Data inválida."));
        results.Should().Contain(r => r.ErrorMessage.Contains("O valor do campo ClienteId da Venda deve ser maior que zero."));
    }

    
    [Fact]
    public void AllDtos_Should_Inherit_From_Valid_BaseClass()
    {
        var baseTypes = new[] { typeof(IResourceDtoBase), typeof(ResourceDtoBaseRoot<>), typeof(ResourceDtoBase) };
        var dtoTypes = Assembly.GetAssembly(typeof(ClienteInsertingDto))
                               .GetTypes()
                               .Where(t => t.Namespace == "Omie.Application.Models" && t.IsClass);
        
        foreach (var type in dtoTypes)
        {
            type.GetInterfaces().Should().Contain(i => baseTypes.Contains(i) || baseTypes.Any(b => b.IsAssignableFrom(type)));
        }
    }
}
