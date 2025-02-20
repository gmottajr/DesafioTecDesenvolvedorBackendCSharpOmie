using System.ComponentModel.DataAnnotations.Schema;
using Omie.Common.Abstractions.Domain.Models;


namespace Omie.Domain.Entities;

public class Item : EntityBaseRoot<long>
{
    public long VendaId { get; set; }
    public short Quantidade { get; set; }
    public Venda? Venda { get; set; }
    public string Produto { get; set; }
    public decimal ValorUnitario { get; set; }
    
    [NotMapped]
    public decimal ValorTotal { get => GetValorTota(); }

    private decimal GetValorTota()
    {
        return Quantidade * ValorUnitario;
    }
}