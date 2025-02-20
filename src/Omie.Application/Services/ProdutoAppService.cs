using Omie.DAL;
using Omie.Application.Models;
using MapsterMapper;
using Omie.Common.Abstractions.Application.Services;
using Omie.Domain.Entities;

namespace Omie.Application.Services;

public class ProdutoAppService : AppServiceBase<ProdutoDto, ProdutoInsertingDto, Produto, long>, IProdutoAppService
{
    public ProdutoAppService(IProdutoRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
