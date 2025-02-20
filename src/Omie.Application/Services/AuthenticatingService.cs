using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Omie.Application.Authenticating;
using Omie.Application.Models;

namespace Omie.Application.Services.Authenticating;

public class AuthenticatingService : IAuthenticatingService
{
    private readonly JwtConfigDto _jwtConfig;

    public AuthenticatingService(JwtConfigDto jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }
    
    public string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.ClientSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.AuthUrl), // Issuer
            new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.ClientId) // Audience
        };

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.AuthUrl,
            audience: _jwtConfig.ClientId,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateCredentials(string username, string password)
    {
        //Fake validation
        return username.Length > 0  && password.Length > 0;
        // it would user Identity User Manager to validate the credentials
    }
}