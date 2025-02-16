using Omie.Domain.Abstractions;
using Omie.Domain.enums;

namespace Omie.Domain;

public class Item : EntityBase
{
    public long VendaId { get; set; }
    public long ProdutoId { get; set; }
    public short Quantidade { get; set; }
    public Venda? Venda { get; set; }
    public Produto? Produto { get; set; }
    public decimal ValorTotal { get; set; }
}