using Omie.Application.Models;
using Omie.Common.Abstractions.Application.Services;

namespace Omie.Application;

public interface IProdutoAppService : IAppServiceBase<ProdutoDto, ProdutoInsertingDto, long>
{

}
