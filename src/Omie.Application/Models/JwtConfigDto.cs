using Omie.Common.Abstractions.Application.Models;

namespace Omie.Application.Models;

public class JwtConfigDto : IResourceDtoBase
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}