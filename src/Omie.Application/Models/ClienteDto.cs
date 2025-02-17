using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class ClienteDto : ResourceDtoBaseRoot<long>
{
    [Required]
    [StringLength(100)]
   public string Nome { get; set; } = string.Empty;
    
    [Required]
    [StringLength(11)]
    public string CPF { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 4, ErrorMessage = "Status inválido")]
    public short Status { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber)]
    [Required]
    public string Telefone { get; set; } = string.Empty;
    public long? EnderecoId { get; set; }
    public string? Observacao { get; set; }
}
