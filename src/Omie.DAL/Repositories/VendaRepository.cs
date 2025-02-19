using Omie.DAL.Abstractions;
using Omie.Domain;
using Microsoft.EntityFrameworkCore;
using Omie.Domain.Entities;

namespace Omie.DAL;

public class VendaRepository : DataRepositoryBase<Venda, long>, IVendaRepository
{
    public VendaRepository(DbContextOmie context) : base(context)
    {
    }

    public async Task<Produto> GetProdutoByIdAsync(long produtoId)
    {
        var produto = await ((DbContextOmie)_context).Produtos.FindAsync(produtoId);
        if (produto == null)
        {
            throw new KeyNotFoundException($"Erro: Produto  ID {produtoId} não enconrado.");
        }
        return produto;
    }
}
