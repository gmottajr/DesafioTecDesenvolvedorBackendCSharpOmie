using System.ComponentModel.DataAnnotations;
using Omie.Common.Abstractions.Application.Models;

namespace Omie.Application.Models;

public class ProdutoInsertingDto : IResourceDtoBase
{
    [Required]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "O valor do campo Nome do Produto deve ter entre 3 e 80 caracteres.")]
    public string Nome { get; set; }
    
    [Required]
    [Display(Name = "Descrição")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O valor do campo Descrição do Produto deve ter entre 3 e 500 caracteres.")]
    public string Descricao { get; set; }
    
    [Required]
    [DataType(DataType.Currency)]
    [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Valor do produto deve ser maior que zero.")]
    public decimal Valor { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "O valor do campo Categoria do Produto deve ter entre 4 e 50 caracteres.")]
    public string Categoria { get; set; }
    
    [Required]
    [RegularExpression(@"^(http(s)?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?$", ErrorMessage = "O valor do campo imagem precisa ser uma URL válida.")]
    [DataType(DataType.ImageUrl, ErrorMessage = "O valor do campo imagem precisa ser uma URL válida.")]
    [StringLength(125, ErrorMessage = "O valor do campo Imagem não pode ultrapassar 125 caracteres.")]
    public string Imagem { get; set; }
    
    [Required]
    [Display(Name = "Marca do Produto")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "O valor do campo Marca do Produto deve ter entre 2 e 30 caracteres.")]
    public string Marca { get; set; }
    
    [Required]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "O valor do campo Unidade do Produto deve ter entre 1 e 10 caracteres.")]
    [RegularExpression(@"^[A-Za-z]{1,5}\d{0,3}$", ErrorMessage = "Unidade deve começar com letras e pode ter até 3 números no final.")]
    public string Unidade { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "O valor do campo Tipo do Produto deve ter entre 3 e 30 caracteres.")]
    public string Tipo { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "O valor do campo Código do Produto deve ter 12 caracteres.")]
    [RegularExpression(@"^[A-Z]{5}\d{7}$", ErrorMessage = "Código deve começar com 5 letras maiúsculas seguidas de 7 dígitos numéricos.")]
    public string Codigo { get; set; }
}
