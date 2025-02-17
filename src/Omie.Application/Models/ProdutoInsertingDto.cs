using System.ComponentModel.DataAnnotations;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public class ProdutoInsertingDto: IResourceDtoBase
{
    [Required]
    [MaxLength(80)]
    [MinLength(3)]
    public string Nome { get; set; }
    
    [Required]
    [MaxLength(500)]
    [MinLength(3)]
    public string Descricao { get; set; }
    
    [Required]
    [DataType(DataType.Currency)]
    public decimal Valor { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Categoria { get; set; }
    
    [Required]
    [DataType(DataType.ImageUrl)]
    [MaxLength(125)]
    public string Imagem { get; set; }
    
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string Marca { get; set; }
    
    [Required]
    [MaxLength(10)]
    [MinLength(1)]
    public string Unidade { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Tipo { get; set; }

    [Required]
    [MaxLength(12)]
    [MinLength(12)]
    public string Codigo { get; set; }
}
