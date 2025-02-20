using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models;

namespace Omie.WebApi;

[ApiController]
[Route("api/[controller]")]
public class VendaController : OmieVendasBaseController<VendaDto, VendaInsertingDto, long>
{
    public VendaController(IVendaAppService applicationService) : base(applicationService)
    {
    }
}
