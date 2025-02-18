namespace Omie.Domain.Abstractions;

public abstract class EntityBaseRoot<TKey> : EntityBase
{
    public TKey? Id { get; set; }
}

