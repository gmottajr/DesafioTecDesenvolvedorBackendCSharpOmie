namespace Omie.Common.Abstractions.Domain.Models;

public abstract class EntityBase
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual DateTime? UpdatedAt { get; set; }
}
