

using Omie.Common.Abstractions.Domain.Models;

namespace Omie.Domain.Entities;

public class Produto : EntityBaseRoot<long>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string? Imagem { get; set; }
    public string? Categoria { get; set; }
    public string? Marca { get; set; }
    public string? Unidade { get; set; }
    public string? Tipo { get; set; }
    public string? Codigo { get; set; }
    public long? EstoqueId { get; set; }
}