using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public class VendaInsertingDto: IResourceDtoBase
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DataDaVenda { get; set; }
    
    [Required]
    public long ClienteId { get; set; }
     
    [Required]
    public List<ItemInsertingDto>? Itens { get; set; }
}
