using System.ComponentModel.DataAnnotations;
using Omie.Common.Abstractions.Application.Models;

namespace Omie.Application.Models;

public class VendaDto : ResourceDtoBaseRoot<long>
{
    [Required]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "O campo Cliente deve ter entre 2 e 40 caracteres.")]
    public string Cliente { get; set; }
    
    [Required]
    [DataType(DataType.DateTime, ErrorMessage = "Data inválida.")]
    public DateTime DataDaVenda { get; set; }

    public List<ItemDto> Itens { get; set; }

    [Required]
    public string CodigoVenda { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

}
