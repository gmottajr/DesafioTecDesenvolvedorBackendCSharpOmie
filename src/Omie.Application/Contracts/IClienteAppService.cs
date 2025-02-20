using Omie.Application.Models;
using Omie.Common.Abstractions.Application.Services;


namespace Omie.Application;


public interface IClienteAppService : IAppServiceBase<ClienteDto, ClienteInsertingDto, long>
{

}