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
    [StringLength(40, MinimumLength = 2, ErrorMessage = "O campo Cliente deve ter entre 2 e 40 caracteres.")]
    public string Cliente { get; set; }
     
    [Required]
    public List<ItemInsertingDto>? Itens { get; set; }
}
