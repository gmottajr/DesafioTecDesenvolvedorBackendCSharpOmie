using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models;
using Omie.Common.Abstractions.Presentation.Controllers;

namespace Omie.WebApi;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : OmieCrudBaseController<ProdutoDto, ProdutoInsertingDto, long>
{
    public ProdutoController(IProdutoAppService applicationService) : base(applicationService)
    {
    }
}
