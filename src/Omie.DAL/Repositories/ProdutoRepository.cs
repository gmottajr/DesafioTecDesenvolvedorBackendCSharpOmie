using Omie.Common.Abstractions.DAL.Reposotories;
using Omie.Domain.Entities;

namespace Omie.DAL;

public class ProdutoRepository : DataRepositoryBase<Produto, long>, IProdutoRepository
{
    public ProdutoRepository(DbContextOmie context) : base(context)
    {
    }
}
