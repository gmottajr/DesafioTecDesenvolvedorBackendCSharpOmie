using Omie.DAL.Abstractions;
using Omie.Domain;
using Microsoft.EntityFrameworkCore;

namespace Omie.DAL;

public class ProdutoRepository : DataRepositoryBase<Produto, long>, IProdutoRepository
{
    public ProdutoRepository(DbContextOmie context) : base(context)
    {
    }
}
