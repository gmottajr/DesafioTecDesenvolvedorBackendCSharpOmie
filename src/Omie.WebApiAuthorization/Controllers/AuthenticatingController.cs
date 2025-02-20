using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Authenticating;
using Omie.WebApiAuthorization.Models;

namespace Authenticating.WebApi.Controllers;

[Route("api/Authentic/[controller]")]
[ApiController]
public class AuthenticatingController : ControllerBase
{
    private readonly IAuthenticatingService _authenticatingService;
    private readonly IConfiguration _configuration;

    public AuthenticatingController(IAuthenticatingService authenticatingService,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _authenticatingService = authenticatingService;
    }
    
    [HttpPost("token")]
    public IActionResult GetToken([FromBody] LoginRequestDto loginRequest)
    {
        var isValid = _authenticatingService.ValidateCredentials(loginRequest.Username, loginRequest.Password);
        if (!isValid)
        {
            return Unauthorized("Invalid credentials");
        }
        var token = _authenticatingService.GenerateJwtToken(loginRequest.Username);
        return Ok(new { Token = token });
    }
}

