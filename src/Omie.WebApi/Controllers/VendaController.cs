using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models;
using Omie.Common.Abstractions.Presentation.Controllers;

namespace Omie.WebApi;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VendaController : OmieCrudBaseController<VendaDto, VendaInsertingDto, long>
{
    public VendaController(IVendaAppService applicationService) : base(applicationService)
    {
    }
}
