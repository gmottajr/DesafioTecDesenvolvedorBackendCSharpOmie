using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OmieVendas.WebApiClientes.Models;

namespace MyApp.Omie.WebApi.Clientes;

/// <summary>
/// Use this controller to authenticate users.
/// This controller is used to generate a JWT token for the user.
/// The login credentials are hardcoded in the controller as username: test@test.com and password: test@123.
/// The token is valid for 20 minutes.
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Random _random = new Random();

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("token")]
    public async Task<IActionResult> Post([FromBody]LoginModelDto login)
    {
        if (login == null)
            return BadRequest("Invalid client request");
        if (login.UserName == "test@test.com" && login.Password == "test@123")
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", $"{_random.Next(1, 1000)}"),
                    new Claim(ClaimTypes.Name, login.UserName)
                })
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);
        }
        return Unauthorized();
    }
}

