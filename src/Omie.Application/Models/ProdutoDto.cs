using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Omie.Application.Models;
using Omie.Application.Models.Abstractions;

namespace Omie.Application.Models;

public class ProdutoDto : ResourceDtoBaseRoot<long>
{
    [Required]
    [MaxLength(80, ErrorMessage = "O valor do campo Nome do Produto não pode ultrapassar 80 caracteres.")]
    [MinLength(3, ErrorMessage = "O valor do campo Nome do Produto não pode ser menor que 3 caracteres.")]
    public string Nome { get; set; }
    
    [Required]
    [MaxLength(500, ErrorMessage = "A Descrição do Produto não pode ultrapassar 500 caracteres.")]
    [MinLength(3, ErrorMessage = "O valor do campo Descrição do Produto não pode ser menor que 3 caracteres.")]
    public string Descricao { get; set; }
    
    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, 1000000000, ErrorMessage = "Valor do produto deve ser maior que zero")]
    public decimal Valor { get; set; }
    
    [Required]
    [MaxLength(50, ErrorMessage = "A categoria do produto não pode ultrapassar 50 caracteres.")]
    [MinLength(4, ErrorMessage = "O valor do campo Categoria do Produto não pode ser menor que 4 caracteres.")]
    public string Categoria { get; set; }
    
    [Required]
    [DataType(DataType.ImageUrl, ErrorMessage = "O valor do campo imagem precisa ser uma URL válida.")]
    [MaxLength(125, ErrorMessage = "O valor do campo Imagem não pode ultrapassar 125 caracteres.")]
    public string Imagem { get; set; }
    
    [Required]
    [MaxLength(30, ErrorMessage = "O valor do campo Marca não pode ultrapassar 30 caracteres.")]
    [MinLength(2, ErrorMessage = "O valor do campo Marca não pode ser menor que 2 caracteres.")]
    public string Marca { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "O valor do campo Unidade não pode ultrapassar 10 caracteres.")]
    [MinLength(1, ErrorMessage = "O valor do campo Unidade não pode ser menor que 1 caracter.")]
    public string Unidade { get; set; }
    
    [Required]
    [MaxLength(30, ErrorMessage = "O valor do campo Tipo não pode ultrapassar 30 caracteres.")]
    [MinLength(3, ErrorMessage = "O valor do vampo Tipo não pode ser menor que 3 caracteres.")]
    public string Tipo { get; set; }

    [Required]
    [MaxLength(12, ErrorMessage = "O valor do campo Código pode ultrapassar 12 caracteres.")]
    [MinLength(12, ErrorMessage = "O valor do campo Código pode ser menor que 12 caracteres.")]
    public string Codigo { get; set; }
    
}
