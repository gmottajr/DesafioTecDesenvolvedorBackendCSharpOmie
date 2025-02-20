using Microsoft.AspNetCore.Authorization;
using Omie.Application;
using Omie.Application.Models;
using Omie.Common.Abstractions.Presentation.Controllers;


namespace Omie.WebApi.Clientes;

[Authorize]
public class ClienteController : OmieCrudBaseController<ClienteDto ,ClienteInsertingDto, long>
{
    public ClienteController(IClienteAppService applicationService) : base(applicationService)
    {
    }
    
}
