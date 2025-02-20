
using Omie.Common.Abstractions.Domain.Models;
using Omie.Domain.enums;

namespace Omie.Domain.Entities;

public class Cliente : EntityBaseRoot<long>
{
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public ClienteStatusEnum Status { get; set; } = ClienteStatusEnum.Ativo;
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public long? EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }
    public string? Observacao { get; set; }

}
