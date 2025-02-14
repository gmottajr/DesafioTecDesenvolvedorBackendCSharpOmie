using System.ComponentModel.DataAnnotations;
using Omie.Domain.enums;
using Omie.Application.Models.Abstractions;
namespace Omie.Application.Models
{
    public class ItemDto: ResourceDtoBase
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long IdVenda { get; set; }
        [Required]
        public long IdProduto { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public decimal ValorUnitario { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        [Required]
        public decimal Desconto { get; set; }
        [Required]
        public decimal ValorDesconto { get; set; }
        [Required]
        public decimal ValorLiquido { get; set; }
        [Required]
        public decimal ValorImpostos { get; set; }
        [Required]
        public decimal ValorAcrescimos { get; set; }
        [Required]
        public decimal ValorFinal { get; set; }
        [Required]
        public decimal ValorComissao { get; set; }
        [Required]
        public decimal ValorCusto { get; set; }
    }
}
