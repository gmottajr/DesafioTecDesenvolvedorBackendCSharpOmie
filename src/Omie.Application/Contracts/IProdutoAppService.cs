using Omie.Application.Models;
using Omie.Domain;

namespace Omie.Application;

public interface IProdutoAppService : IAppServiceBase<ProdutoDto, ProdutoInsertingDto, long>
{

}
