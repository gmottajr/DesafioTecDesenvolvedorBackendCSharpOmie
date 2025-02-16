using Omie.Application.Models;
using Omie.Domain;

namespace Omie.Application;

public interface IVendaAppService : IAppServiceBase<VendaDto, VendaInsertingDto, long>
{

}
