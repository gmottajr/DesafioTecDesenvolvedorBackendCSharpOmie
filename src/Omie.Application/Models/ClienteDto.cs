using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class ClienteDto : ResourceDtoBaseRoot<long>
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O valor do campo Nome do Cliente deve ter entre 2 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O valor do campo CPF deve de exatos 11 caracteres.")]
    public string CPF { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 4, ErrorMessage = "O valor do campo Status do Cliente deve ser entre 1 e 4 caracteres.")]
    public short Status { get; set; }

    [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail inválido.")]
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "O valor do campo E-Mail deve ter entre 4 e 100 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber, ErrorMessage = "Telefone inválido")]
    [Required]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "O valor do campo Telefone deve ter entre 10 e 20 caracteres.")]
    public string Telefone { get; set; }
    public long? EnderecoId { get; set; }
    public string? Observacao { get; set; }
}
