using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Omie.Application.Models;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class VendaDto : ResourceDtoBaseRoot<long>
{
    [Required]
    public long ClienteId { get; set; }
    
    [Required]
    [DataType(DataType.DateTime, ErrorMessage = "Data inválida.")]
    public DateTime DataDaVenda { get; set; }

    public List<ItemDto> Itens { get; set; }

    [Required]
    public string CodigoVenda { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    
    public ClienteDto Cliente { get; set; }
    
    [Required]
    public List<ItemDto> Items { get; set; }

}
