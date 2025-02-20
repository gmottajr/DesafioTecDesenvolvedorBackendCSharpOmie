

using Omie.Common.Abstractions.Domain.Models;

namespace Omie.Domain.Entities;

public class Venda : EntityBaseRoot<long>
{
    public DateTime DataDaVenda { get; set; } = DateTime.Now;
    public string Cliente { get; set; }
    public string CodigoVenda { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public List<Item>? Itens { get; set; } = new List<Item>();
}