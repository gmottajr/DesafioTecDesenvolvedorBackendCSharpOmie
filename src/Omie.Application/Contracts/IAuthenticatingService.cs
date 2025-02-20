using Omie.Application.Models;

namespace Omie.Application.Authenticating;

public interface IAuthenticatingService
{
    string GenerateJwtToken(string username);
    bool ValidateCredentials(string username, string password);
}