namespace Omie.Common.Abstractions.Domain.Models;

public abstract class EntityBaseRoot<TKey> : EntityBase
{
    public TKey? Id { get; set; }
}

