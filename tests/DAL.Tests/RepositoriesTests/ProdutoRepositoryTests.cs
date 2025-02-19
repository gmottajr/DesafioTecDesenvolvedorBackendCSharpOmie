using Omie.DAL;
using Omie.Domain.Entities;
using Tests.Common.Fixtures;

namespace DAL.Tests;

public class ProdutoRepositoryTests : IClassFixture<DatabaseFixture<Produto, DbContextOmie>>
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly DatabaseFixture<Produto, DbContextOmie> _fixture;

    public ProdutoRepositoryTests(DatabaseFixture<Produto, DbContextOmie> fixture)
    {
        _fixture = fixture;
        _produtoRepository = new ProdutoRepository(_fixture.Context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProdutoSuccessfully()
    {
        var produtos = _fixture.GetEntities<Produto>(3, true);
        foreach (var produto in produtos)
        {
            var prod = await AddOneProduct(produto);
            var addedProduto = await _produtoRepository.GetByIdAsync(prod.Id);
            Assert.NotNull(addedProduto);
            Assert.Equal(prod.Id, addedProduto?.Id);
        }
    }

    private async Task<Produto> AddOneProduct(Produto produto)
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProdutoById()
    {
        var produtos = _fixture.GetEntities<Produto>(3, true);
        foreach (var produto in produtos)
        {
            var prod = await AddOneProduct(produto);
            var addedProduto = await _produtoRepository.GetByIdAsync(prod.Id);
            Assert.NotNull(addedProduto);
            Assert.Equal(prod.Id, addedProduto?.Id);
        }
    }

    [Fact]
    public async Task Update_ShouldUpdateProdutoSuccessfully()
    {
        var produtos = _fixture.GetEntities<Produto>(3, true);
        foreach (var produto in produtos)
        {
            var prod = await AddOneProduct(produto);
            prod.Nome = "Updated Value"; // Example of update
            _produtoRepository.Update(prod);
            await _produtoRepository.SaveChangesAsync();

            var updatedProduto = await _produtoRepository.GetByIdAsync(prod.Id);
            Assert.NotNull(updatedProduto);
            Assert.Equal("Updated Value", updatedProduto?.Nome);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProdutoSuccessfully()
    {
        var produto = _fixture.GetEntities<Produto>(3, true);
        foreach(var prod in produto)
        {
            await AddOneProduct(prod);
            await _produtoRepository.DeleteAsync(prod.Id);
            await _produtoRepository.SaveChangesAsync();
            var deletedProduto = await _produtoRepository.GetByIdAsync(prod.Id);
            Assert.Null(deletedProduto);
        }
    }
}

