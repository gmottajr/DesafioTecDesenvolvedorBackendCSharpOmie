using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class ItemInsertingDto: IResourceDtoBase
{
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "O valor do campo VendaId do item da Venda deve ser maior que zero.")]
    public long IdVenda { get; set; }
    
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "O valor do campo ProdutoId do item da Venda deve ser maior que zero.")]
    public long IdProduto { get; set; }
    
    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "O valor do campo Quatidade do item da Venda deve ser maior que zero.")]
    public decimal Quantidade { get; set; }
    
}
