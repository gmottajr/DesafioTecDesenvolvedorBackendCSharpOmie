using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class VendaInsertingDto: IResourceDtoBase
{
    [Required]
    [DataType(DataType.DateTime, ErrorMessage = "Data inválida.")]
    [Range(typeof(DateTime), "2002-01-01", "2090-12-31", ErrorMessage = "Data inválida.")]
    public DateTime DataDaVenda { get; set; }
    
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "O valor do campo ClienteId da Venda deve ser maior que zero.")]
    public long ClienteId { get; set; }
     
    [Required]
    public List<ItemInsertingDto>? Itens { get; set; }
}
