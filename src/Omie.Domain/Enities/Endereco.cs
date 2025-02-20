

using Omie.Common.Abstractions.Domain.Models;

namespace Omie.Domain.Entities;

public class Endereco : EntityBaseRoot<long>
{
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public long ClienteId { get; set; }
}