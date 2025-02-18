using FluentAssertions;
using Omie.DAL;
using Omie.Domain.Entities;
using Omie.WebApi;
using Tests.Common.Data;
using Tests.Common.Fixtures;

namespace Database.Tests;

public class ProdutoTests : IClassFixture<DatabaseFixture<DbContextOmie, ProdutoController>>
{
    private readonly DatabaseFixture<DbContextOmie, ProdutoController> _fixture;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoTests(DatabaseFixture<DbContextOmie, ProdutoController> fixture)
    {
        _fixture = fixture;
        _produtoRepository = new ProdutoRepository(fixture.Context);
    }

    [Fact]
    public async Task Test_CreateProduto()
    {
        var produto = await AddOneProduto();
        var retrievedProduto = await _produtoRepository.GetByIdAsync(produto.Id);
        retrievedProduto.Should().NotBeNull();
        retrievedProduto.Nome.Should().Be(produto.Nome);
        retrievedProduto.Id.Should().Be(produto.Id);
        retrievedProduto.Codigo.Should().Be(produto.Codigo);
        retrievedProduto.Valor.Should().Be(produto.Valor);
    }

    [Fact]
    public async Task AddinManyProdutosShouldBehaveAsExpectedAndPersistAllRecordsSuccessfully()
    {
        for(int i = 0; i < 15; i++)
        {
            var produto = await AddOneProduto();
            var retrievedProduto = await _produtoRepository.GetByIdAsync(produto.Id);
            retrievedProduto.Should().NotBeNull();
            retrievedProduto.Nome.Should().Be(produto.Nome);
            retrievedProduto.Id.Should().Be(produto.Id);
            retrievedProduto.Codigo.Should().Be(produto.Codigo);
            retrievedProduto.Valor.Should().Be(produto.Valor);
        }
    }

    private async Task<Produto> AddOneProduto()
    {
        var produto = TestData.GetProduto(_fixture);
        produto.Nome = produto.Nome.Substring(0, 20); 
        produto.Descricao = produto.Descricao.Substring(0, 14); 
        produto.Codigo = produto.Codigo.Substring(0, 12);
        produto.Imagem = produto.Imagem.Substring(0, 12); 
        produto.Categoria = produto.Categoria.Substring(0, 10); 
        produto.Marca = produto.Marca.Substring(0, 10); 
        produto.Unidade = produto.Unidade.Substring(0, 5); 
        produto.Tipo = produto.Tipo?.Substring(0, 10); 
        produto.Valor = 100m; 

        await _produtoRepository.AddAsync(produto);
        await _produtoRepository.SaveChangesAsync();
        return produto;
    }

    [Fact]
    public async Task Test_ReadProduto()
    {
        var produto = await AddOneProduto();
        var allProdutos = await _produtoRepository.GetAllAsync();
        allProdutos.Should().NotBeNull();
        allProdutos.Count().Should().BeGreaterThan(0);
        allProdutos.Should().Contain(p => p.Id == produto.Id);
    }

    [Fact]
    public async Task Test_DeleteProduto()
    {
        var produto = await AddOneProduto();
        await _produtoRepository.DeleteAsync(produto.Id);
        await _produtoRepository.SaveChangesAsync();
        var deletedProduto = await _produtoRepository.GetByIdAsync(produto.Id);
        deletedProduto.Should().BeNull();
    }

    [Fact]
    public async Task Test_UpdateProduto()
    {
        var produto = await AddOneProduto();
        var newName = "Updated Produto Name";
        produto.Nome = newName;
        await _produtoRepository.SaveChangesAsync();
        var updatedProduto = await _produtoRepository.GetByIdAsync(produto.Id);
        updatedProduto.Nome.Should().Be(newName);
    }
}
