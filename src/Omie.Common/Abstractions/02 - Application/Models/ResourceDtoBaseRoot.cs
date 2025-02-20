namespace Omie.Common.Abstractions.Application.Models;

public class ResourceDtoBaseRoot<TKey> : ResourceDtoBase
{
    public TKey Id { get; set; }
}
