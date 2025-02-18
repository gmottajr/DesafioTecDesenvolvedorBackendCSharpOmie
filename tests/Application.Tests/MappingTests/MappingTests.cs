using FluentAssertions;
using Mapster;
using Omie.Application.Models;
using Omie.Application.Services;
using Omie.Domain;
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
            VendaId = 1,
            ProdutoId = 2,
            Quantidade = 3,
            ValorUnitario = 10.5m
        };
        // Act
        var entity = dto.Adapt<Item>();
        // Assert
        entity.Should().NotBeNull();
        entity.VendaId.Should().Be(dto.VendaId);
        entity.ProdutoId.Should().Be(dto.ProdutoId);
        entity.Quantidade.Should().Be(dto.Quantidade);
    }
    
    [Fact]
    public void Item_To_ItemDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Item
        {
            VendaId = 1,
            ProdutoId = 2,
            Quantidade = 3,
            ValorTotal = 31.5m 
        };
        // Act
        var dto = entity.Adapt<ItemDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.VendaId.Should().Be(entity.VendaId);
        dto.ProdutoId.Should().Be(entity.ProdutoId);
        dto.Quantidade.Should().Be(entity.Quantidade);
    }
    
    [Fact]
    public void Venda_To_VendaDto_Should_Map_Correctly()
    {
        // Arrange
        var entity = new Venda
        {
            Id = 1,
            ClienteId = 2,
            DataDaVenda = DateTime.Now,
            CodigoVenda = "V001",
            Itens = new List<Item> { new Item { VendaId = 1, ProdutoId = 3, Quantidade = 1, ValorTotal = 10m } }
        };
        // Act
        var dto = entity.Adapt<VendaDto>();
        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(entity.Id);
        dto.ClienteId.Should().Be(entity.ClienteId);
        dto.DataDaVenda.Should().Be(entity.DataDaVenda);
        dto.CodigoVenda.Should().Be(entity.CodigoVenda);
        dto.Itens.Should().NotBeNull().And.HaveCount(1);
        dto.Itens.First().VendaId.Should().Be(1);
        dto.Itens.First().ProdutoId.Should().Be(3);
        dto.Itens.First().Quantidade.Should().Be(1);
        // Note: ValorUnitario might need special handling if not directly mapped
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
        // Arrange
        var dto = new VendaDto
        {
            Id = 1,
            ClienteId = 2,
            DataDaVenda = DateTime.Now,
            CodigoVenda = "V001",
            Itens = new List<ItemDto> { new ItemDto { VendaId = 1, ProdutoId = 3, Quantidade = 1, ValorUnitario = 10m } }
        };
        // Act
        var entity = dto.Adapt<Venda>();
        // Assert
        entity.Should().NotBeNull();
        entity.Id.Should().Be(dto.Id);
        entity.ClienteId.Should().Be(dto.ClienteId);
        entity.DataDaVenda.Should().Be(dto.DataDaVenda);
        entity.CodigoVenda.Should().Be(dto.CodigoVenda);
        entity.Itens.Should().NotBeNull().And.HaveCount(1);
        entity.Itens.First().VendaId.Should().Be(1);
        entity.Itens.First().ProdutoId.Should().Be(3);
        entity.Itens.First().Quantidade.Should().Be(1);
        // Note: ValorTotal might be calculated or set separately
    }
    
    [Fact]
    public void VendaInsertingDto_To_Venda_Should_Map_Correctly()
    {
        // Arrange
        var dto = new VendaInsertingDto
        {
            ClienteId = 2,
            DataDaVenda = DateTime.Now,
            Itens = new List<ItemInsertingDto> { new ItemInsertingDto { VendaId = 1, ProdutoId = 3, Quantidade = 1m } }
        };
        // Act
        var entity = dto.Adapt<Venda>();
        // Assert
        entity.Should().NotBeNull();
        entity.ClienteId.Should().Be(dto.ClienteId);
        entity.DataDaVenda.Should().Be(dto.DataDaVenda);
        entity.Itens.Should().NotBeNull().And.HaveCount(1);
        entity.Itens.First().VendaId.Should().Be(1);
        entity.Itens.First().ProdutoId.Should().Be(3);
        entity.Itens.First().Quantidade.Should().Be(1); // Note: Here we might need type conversion if Quantidade is short in Item
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
