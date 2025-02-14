using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Omie.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GmProdutos : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Hello World", Id = 1, Name = "GmProdutos" });
    }
}

