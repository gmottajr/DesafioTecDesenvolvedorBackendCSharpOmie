using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;
public class ItemDto: ResourceDtoBaseRoot<long>
{
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "O valor do campo VendaId do item da Venda deve ser maior que zero.")]
    public long VendaId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo Produto deve ter entre 2 e 100 caracteres.")]
    public string Produto { get; set; }
    
    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "O valor do campo Quatidade do item da Venda deve ser maior que zero.")]
    public short Quantidade { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do campo ValorUnitario do item da Venda deve ser maior que zero.")]
    public decimal ValorUnitario { get; set; }

    public decimal ValorTotal => Quantidade * ValorUnitario;
}

