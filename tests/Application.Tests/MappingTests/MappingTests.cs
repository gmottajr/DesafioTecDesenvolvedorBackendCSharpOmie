using AutoFixture;
using FluentAssertions;
using Mapster;
using Omie.Application.Models;
using Omie.Application.Services;
using Omie.Domain.Entities;
using Omie.Domain.enums;

namespace Application.Tests;

public class MappingTests
{
    public MappingTests()
    {
        var omieApplicationAssembly = typeof(VendaAppService).Assembly;
        TypeAdapterConfig.GlobalSettings.Scan(omieApplicationAssembly);
    }
    
    [Fact]
    public void ClienteInsertingDto_To_Cliente_Should_Map_Correctly()
    {
        // Arrange
        var dto = new ClienteInsertingDto
        {
            Nome = "John Doe",
            CPF = "12345678901",
            Status = 1,
            Email = "john@example.com",
            Telefone = "1234567890",
            EnderecoId = 1,
            Observacao = "Test Observation"
        };
        // Act
        var entity = dto.Adapt<Cliente>();
        // Assert
        entity.Should().NotBeNull();
        entity.Nome.Should().Be(dto.Nome);
        entity.CPF.Should().Be(dto.CPF);
        entity.Status.Should().Be((ClienteStatusEnum)dto.Status); 
        entity.Email.Should().Be(dto.Email);
        entity.Telefone.Should().Be(dto.Telefone);
        entity.EnderecoId.Should().Be(dto.EnderecoId);
        entity.Observacao.Should().Be(dto.Observacao);
    }
    
    [Fact]
    public void Cliente_To_ClienteInsertingDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Cliente
        {
            Id = 1, // Note: Id might not be mapped back to ClienteInsertingDto if not needed
            Nome = "Jane Doe",
            CPF = "12345678901",
            Status = ClienteStatusEnum.Ativo,
            Email = "jane@example.com",
            Telefone = "1234567890",
            EnderecoId = 1,
            Observacao = "Test Observation"
        };
        // Act
        var dto = entity.Adapt<ClienteInsertingDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Nome.Should().Be(entity.Nome);
        dto.CPF.Should().Be(entity.CPF);
        dto.Status.Should().Be((short)entity.Status); 
        dto.Email.Should().Be(entity.Email);
        dto.Telefone.Should().Be(entity.Telefone);
        dto.EnderecoId.Should().Be(entity.EnderecoId);
        dto.Observacao.Should().Be(entity.Observacao);
    }
    
    [Fact]
    public void ClienteDto_To_Cliente_Should_Map_Correctly()
    {
        // Arrange
        var dto = new ClienteDto
        {
            Id = 1,
            Nome = "John Doe",
            CPF = "12345678901",
            Status = 1,
            Email = "john@example.com",
            Telefone = "1234567890",
            EnderecoId = 1,
            Observacao = "Test Observation"
        };
        // Act
        var entity = dto.Adapt<Cliente>();
        // Assert
        entity.Should().NotBeNull();
        entity.Id.Should().Be(dto.Id);
        entity.Nome.Should().Be(dto.Nome);
        entity.CPF.Should().Be(dto.CPF);
        entity.Status.Should().Be((ClienteStatusEnum)dto.Status); 
        entity.Email.Should().Be(dto.Email);
        entity.Telefone.Should().Be(dto.Telefone);
        entity.EnderecoId.Should().Be(dto.EnderecoId);
        entity.Observacao.Should().Be(dto.Observacao);
    }
    
    [Fact]
    public void Cliente_To_ClienteDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Cliente
        {
            Id = 1,
            Nome = "Jane Doe",
            CPF = "12345678901",
            Status = ClienteStatusEnum.Ativo,
            Email = "jane@example.com",
            Telefone = "1234567890",
            EnderecoId = 1,
            Observacao = "Test Observation"
        };
        // Act
        var dto = entity.Adapt<ClienteDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(entity.Id);
        dto.Nome.Should().Be(entity.Nome);
        dto.CPF.Should().Be(entity.CPF);
        dto.Status.Should().Be((short)entity.Status); 
        dto.Email.Should().Be(entity.Email);
        dto.Telefone.Should().Be(entity.Telefone);
        dto.EnderecoId.Should().Be(entity.EnderecoId);
        dto.Observacao.Should().Be(entity.Observacao);
    }
    
    [Fact]
    public void ItemDto_To_Item_Should_Map_Correctly()
    {
        // Arrange
        var dto = new ItemDto
        {
            Id = new Fixture().Create<long>(),
            VendaId = new Fixture().Create<long>(),
            Produto = new Fixture().Create<string>(),
            Quantidade = new Fixture().Create<short>(),
            ValorUnitario = new Fixture().Create<decimal>()
        };
        // Act
        var entity = dto.Adapt<Item>();
        // Assert
        entity.Should().NotBeNull();
        entity.VendaId.Should().Be(dto.VendaId);
        entity.Produto.Should().Be(dto.Produto);
        entity.Quantidade.Should().Be(dto.Quantidade);
    }
    
    [Fact]
    public void Item_To_ItemDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Item
        {
            Id = new Fixture().Create<long>(),
            VendaId = new Fixture().Create<long>(),
            Produto = new Fixture().Create<string>(),
            Quantidade = new Fixture().Create<short>(),
            ValorUnitario = new Fixture().Create<decimal>()
        };
        // Act
        var dto = entity.Adapt<ItemDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(entity.Id);
        dto.VendaId.Should().Be(entity.VendaId);
        dto.Produto.Should().Be(entity.Produto);
        dto.Quantidade.Should().Be(entity.Quantidade);
    }
    
    [Fact]
    public void Venda_To_VendaDto_Should_Map_Correctly()
    {
        // Arrange
        var gotVendaId = new Fixture().Create<long>();
        var gotCliente = new Fixture().Create<string>();
        var gotDataDaVenda = new Fixture().Create<DateTime>();
        var gotCodigoVenda = new Fixture().Create<string>();
        var gotProduto = new Fixture().Create<string>();
        var gotQuantidade = new Fixture().Create<short>();
        var gotValorUnitario = new Fixture().Create<decimal>();

        var entity = new Venda
        {
            Id = gotVendaId,
            Cliente = gotCliente,
            DataDaVenda = gotDataDaVenda,
            CodigoVenda = gotCodigoVenda,
            Itens = new List<Item> { new Item 
            { 
                VendaId = gotVendaId, 
                Produto = gotProduto, 
                Quantidade = gotQuantidade, 
                ValorUnitario = gotValorUnitario 
            } 
            }
        };
        // Act
        var dto = entity.Adapt<VendaDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(entity.Id);
        dto.Cliente.Should().Be(entity.Cliente);
        dto.DataDaVenda.Should().Be(entity.DataDaVenda);
        dto.CodigoVenda.Should().Be(entity.CodigoVenda);
        dto.Itens.Should().NotBeNull().And.HaveCount(1);
        dto.Itens.First().VendaId.Should().Be(gotVendaId);
        dto.Itens.First().Produto.Should().Be(gotProduto);
        dto.Itens.First().Quantidade.Should().Be(gotQuantidade);
    }
    
    [Fact]
    public void Produto_To_ProdutoDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Produto
        {
            Id = 1,
            Nome = "Product Name",
            Descricao = "Product Description",
            Valor = 10.99m,
            Imagem = "image.jpg",
            Categoria = "Category",
            Marca = "Brand",
            Unidade = "Unit",
            Tipo = "Type",
            Codigo = "ABC1234567890"
        };
        // Act
        var dto = entity.Adapt<ProdutoDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(entity.Id);
        dto.Nome.Should().Be(entity.Nome);
        dto.Descricao.Should().Be(entity.Descricao);
        dto.Valor.Should().Be(entity.Valor);
        dto.Imagem.Should().Be(entity.Imagem);
        dto.Categoria.Should().Be(entity.Categoria);
        dto.Marca.Should().Be(entity.Marca);
        dto.Unidade.Should().Be(entity.Unidade);
        dto.Tipo.Should().Be(entity.Tipo);
        dto.Codigo.Should().Be(entity.Codigo);
    }
    
    [Fact]
    public void ProdutoInsertingDto_To_Produto_Should_Map_Correctly()
    {
        // Arrange
        var dto = new ProdutoInsertingDto
        {
            Nome = "Product Name",
            Descricao = "Product Description",
            Valor = 10.99m,
            Categoria = "Category",
            Imagem = "image.jpg",
            Marca = "Brand",
            Unidade = "Unit",
            Tipo = "Type",
            Codigo = "ABC1234567890"
        };
        // Act
        var entity = dto.Adapt<Produto>();
        // Assert
        entity.Should().NotBeNull();
        entity.Nome.Should().Be(dto.Nome);
        entity.Descricao.Should().Be(dto.Descricao);
        entity.Valor.Should().Be(dto.Valor);
        entity.Imagem.Should().Be(dto.Imagem);
        entity.Categoria.Should().Be(dto.Categoria);
        entity.Marca.Should().Be(dto.Marca);
        entity.Unidade.Should().Be(dto.Unidade);
        entity.Tipo.Should().Be(dto.Tipo);
        entity.Codigo.Should().Be(dto.Codigo);
    }
    
    [Fact]
    public void VendaDto_To_Venda_Should_Map_Correctly()
    {
        var gotVendaId = new Fixture().Create<long>();
        var gotProduto = new Fixture().Create<string>(); 
        var gotQuantidade = new Fixture().Create<short>();
        var gotValorUnitario = new Fixture().Create<decimal>(); 
        // Arrange
        var dto = new VendaDto
        {
            Id = gotVendaId,
            Cliente = new Fixture().Create<string>(),
            DataDaVenda = new Fixture().Create<DateTime>(),
            CodigoVenda = new Fixture().Create<string>(),
            Itens = new List<ItemDto> { new ItemDto { 
                Id = new Fixture().Create<long>(),
                VendaId = gotVendaId, 
                Produto = gotProduto, 
                Quantidade = gotQuantidade, 
                ValorUnitario = gotValorUnitario
                } }
        };
        // Act
        var entity = dto.Adapt<Venda>();
        // Assert
        entity.Should().NotBeNull();
        entity.Id.Should().Be(dto.Id);
        entity.Cliente.Should().Be(dto.Cliente);
        entity.DataDaVenda.Should().Be(dto.DataDaVenda);
        entity.CodigoVenda.Should().Be(dto.CodigoVenda);
        entity.Itens.Should().NotBeNull().And.HaveCount(1);
        entity.Itens.First().Id.Should().Be(dto.Itens.First().Id);
        entity.Itens.First().VendaId.Should().Be(gotVendaId);
        entity.Itens.First().Produto.Should().Be(gotProduto);
        entity.Itens.First().Quantidade.Should().Be(gotQuantidade);
        entity.Itens.First().ValorUnitario.Should().Be(gotValorUnitario);
    }
    
    [Fact]
    public void VendaInsertingDto_To_Venda_Should_Map_Correctly()
    {
        // Arrange
        var gotCliente = new Fixture().Create<string>();
        var gotDataDaVenda = new Fixture().Create<DateTime>();
        var gotProduto = new Fixture().Create<string>();
        var gotQuantidade = new Fixture().Create<short>();
        var gotValorUnitario = new Fixture().Create<decimal>();

        var dto = new VendaInsertingDto
        {
            Cliente = gotCliente,
            DataDaVenda = gotDataDaVenda,
            Itens = new List<ItemInsertingDto> { new ItemInsertingDto
            { 
            ValorUnitario = gotValorUnitario,
            Produto = gotProduto, 
            Quantidade = gotQuantidade
            } }
        };
        // Act
        var entity = dto.Adapt<Venda>();
        // Assert
        entity.Should().NotBeNull();
        entity.Cliente.Should().Be(dto.Cliente);
        entity.DataDaVenda.Should().Be(dto.DataDaVenda);
        entity.Itens.Should().NotBeNull().And.HaveCount(1);
        entity.Itens.First().VendaId.Should().Be(0); 
        entity.Itens.First().Produto.Should().Be(gotProduto);
        entity.Itens.First().Quantidade.Should().Be(gotQuantidade); 
    }
    
    [Fact]
    public void ProdutoDto_To_Produto_Should_Map_Correctly()
    {
        // Arrange
        var dto = new ProdutoDto
        {
            Id = 1,
            Nome = "Product Name",
            Descricao = "Product Description",
            Valor = 10.99m,
            Imagem = "image.jpg",
            Categoria = "Category",
            Marca = "Brand",
            Unidade = "Unit",
            Tipo = "Type",
            Codigo = "ABC1234567890"
        };
        // Act
        var entity = dto.Adapt<Produto>();
        // Assert
        entity.Should().NotBeNull();
        entity.Id.Should().Be(dto.Id);
        entity.Nome.Should().Be(dto.Nome);
        entity.Descricao.Should().Be(dto.Descricao);
        entity.Valor.Should().Be(dto.Valor);
        entity.Imagem.Should().Be(dto.Imagem);
        entity.Categoria.Should().Be(dto.Categoria);
        entity.Marca.Should().Be(dto.Marca);
        entity.Unidade.Should().Be(dto.Unidade);
        entity.Tipo.Should().Be(dto.Tipo);
        entity.Codigo.Should().Be(dto.Codigo);
    }
}
