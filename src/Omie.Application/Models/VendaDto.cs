using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Omie.Application.Models;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class VendaDto : ResourceDtoBaseRoot<long>
{
    [Required]
    public long IdCliente { get; set; }
    
    [Required]
    public long IdFormaPagamento { get; set; }
    [Required]
    public DateTime DataVenda { get; set; }

    public List<ItemDto> Itens { get; set; }

    [Required]
    public string CodigoVenda { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public ClienteDto Cliente { get; set; }

}
