using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models;

namespace Omie.WebApi;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : OmieVendasBaseController<ProdutoDto, long>
{
    public ProdutoController(IProdutoAppService applicationService) : base(applicationService)
    {
    }
}
