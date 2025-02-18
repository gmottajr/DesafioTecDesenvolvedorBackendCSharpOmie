using Omie.Domain.Abstractions;

namespace Omie.Domain.Entities;

public class Venda : EntityBaseRoot<long>
{
    public DateTime DataDaVenda { get; set; } = DateTime.Now;
    public long ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public string CodigoVenda { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public List<Item>? Itens { get; set; } = new List<Item>();
}