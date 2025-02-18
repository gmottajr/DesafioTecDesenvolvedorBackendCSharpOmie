using Omie.Application.Models;
using Omie.Domain;

namespace Omie.Application;

public interface IVendaAppService : IAppServiceBase<VendaDto, VendaInsertingDto, long>
{
    string GeraCodigoVenda(VendaInsertingDto dto);
    Task<bool>  Cancelar(long id);
    Task<bool>  CompletarVenda(long id, DateTime dataCompletado);
    Task<bool> AlterarData(long id, DateTime dataAlterado);
}
