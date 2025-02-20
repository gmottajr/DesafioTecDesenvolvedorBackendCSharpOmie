using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Omie.Application.Authenticating;
using Omie.Application.Models;

namespace Omie.Application.Services.Authenticating;

public class AuthenticatingService : IAuthenticatingService
{
    private readonly JwtConfigDto _jwtConfig;
    private readonly Random _random = new Random();

    public AuthenticatingService(IOptions<JwtConfigDto> jwtConfigOptions)
    {
        _jwtConfig = jwtConfigOptions.Value;
    }
    
    public string GenerateJwtToken(string username)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Expires = DateTime.UtcNow.AddMinutes(120),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.ClientSecret)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.Issuer), // Issuer
                new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.Audience), // Audience
                new Claim("UserId", $"{_random.Next(1, 1000)}"),
                new Claim(ClaimTypes.Name, username)
            })
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

    public bool ValidateCredentials(string username, string password)
    {
        //Fake validation
        return username.Length > 0  && password.Length > 0;
        // it would use Identity User Manager to validate the credentials
    }
}