using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Omie.Application.Models;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class ProdutoDto : ResourceDtoBaseRoot<long>
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Descricao { get; set; }
    [Required]
    public decimal Valor { get; set; }
    [Required]
    public string Categoria { get; set; }
    [Required]
    public string Imagem { get; set; }
    [Required]
    public string Marca { get; set; }
    [Required]
    public string Unidade { get; set; }
    [Required]
    public string Tipo { get; set; }

    [Required]
    public string Codigo { get; set; }
    
}
