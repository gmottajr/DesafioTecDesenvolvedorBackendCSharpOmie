using Omie.DAL;
using Omie.Application.Models;
using Omie.Domain;

namespace Omie.Application.Services;

public class ProdutoAppService : AppServiceBase<ProdutoDto, Produto, long>, IProdutoAppService
{
    public ProdutoAppService(IProdutoRepository repository) : base(repository)
    {
    }
}
