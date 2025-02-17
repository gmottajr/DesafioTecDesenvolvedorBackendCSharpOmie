using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public class ItemInsertingDto: IResourceDtoBase
{
    [Required]
    public long IdVenda { get; set; }
    
    [Required]
    public long IdProduto { get; set; }
    
    [Required]
    public decimal Quantidade { get; set; }
    
    [Required]
    public decimal ValorUnitario { get; set; }
}
