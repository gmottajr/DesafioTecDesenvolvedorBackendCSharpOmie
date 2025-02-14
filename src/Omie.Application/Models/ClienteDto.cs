using System.ComponentModel.DataAnnotations;
using Omie.Domain.enums;
using Omie.Application.Models.Abstractions;
namespace Omie.Application.Models;

public class ClienteDto : ResourceDtoBaseRoot<long>
{
   public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public short Status { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public long? EnderecoId { get; set; }
    public string? Observacao { get; set; }
}
