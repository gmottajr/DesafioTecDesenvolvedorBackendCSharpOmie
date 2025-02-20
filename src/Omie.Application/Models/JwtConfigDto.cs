using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public record JwtConfigDto : IResourceDtoBase
{
    public string AuthUrl { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
}