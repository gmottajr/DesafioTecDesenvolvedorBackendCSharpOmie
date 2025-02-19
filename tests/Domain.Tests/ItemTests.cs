using AutoFixture;
using FluentAssertions;
using Omie.Domain.Entities;

namespace Domain.Tests;

public class ItemTests
{
    [Fact]
    public void Item_Should_Have_Valid_VendaId()
    {
        var item = new Item { VendaId = 1 };
        item.VendaId.Should().BeGreaterThan(0, "Every item must be associated with a valid sale (Venda).");
    }

    [Fact]
    public void Item_Should_Have_Valid_ProdutoId()
    {
        var item = new Item { Produto = "c" };
        item.Produto.Should().NotBeNullOrEmpty("Every item must be associated with a valid product (Produto).");
    }

    [Fact]
    public void Item_Quantidade_Should_Be_Greater_Than_Zero()
    {
        var item = new Item { Quantidade = 1 };
        item.Quantidade.Should().BeGreaterThan(0, "Item quantity must be at least 1.");
    }

    [Fact]
    public void Item_ValorTotal_Should_Be_Calculated_Correctly()
    {
        
        var produto = new Fixture().Create<string>();
        var valorUnitario = new Fixture().Create<decimal>();
        var quantidade = new Fixture().Create<short>();

        var item = new Item { 
                    Produto = produto,
                    ValorUnitario = valorUnitario,
                    Quantidade = quantidade 
                };

        var expectedValorTotal = valorUnitario * quantidade;
        item.ValorTotal.Should().Be(expectedValorTotal, "Total value should be quantity multiplied by product value.");
        item.Produto.Should().Be(produto, "Produto property should be set.");
        item.Quantidade.Should().Be(quantidade, "Quantidade property should be set.");
        item.ValorUnitario.Should().Be(valorUnitario, "ValorUnitario property should be set.");
    }

    [Fact]
    public void Item_Should_Allow_Null_Venda()
    {
        var item = new Item();
        item.Venda.Should().BeNull("Item may not always be linked to a Venda object immediately.");
    }

    [Fact]
    public void Item_Should_Allow_Null_Produto()
    {
        var item = new Item();
        item.Produto.Should().BeNull("Item may not always be linked to a Produto object immediately.");
    }
}