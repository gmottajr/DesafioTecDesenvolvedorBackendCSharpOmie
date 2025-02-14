using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models;


namespace Omie.WebApi;


[ApiController]
[Route("api/[controller]")]
public class ClienteController : OmieVendasBaseController<ClienteDto, long>
{
    public ClienteController(IClienteAppService applicationService) : base(applicationService)
    {
    }
    
}
