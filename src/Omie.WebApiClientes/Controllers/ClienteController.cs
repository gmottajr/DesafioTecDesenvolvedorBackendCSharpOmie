using Microsoft.AspNetCore.Authorization;
using Omie.Application;
using Omie.Application.Models;
using Omie.WebApi.Clientes.Abstractions;


namespace Omie.WebApi.Clientes;

[Authorize]
public class ClienteController : OmieVendasBaseController<ClienteDto ,ClienteInsertingDto, long>
{
    public ClienteController(IClienteAppService applicationService) : base(applicationService)
    {
    }
    
}
