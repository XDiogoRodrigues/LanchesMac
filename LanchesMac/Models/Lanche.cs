using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

[Table("Lanches")]
public class Lanche
{
    [Key]
    public int LancheId { get; set; }

    [Required(ErrorMessage ="O nome do lanche deve ser informado")]
    [Display(Name = "Nome do Lanche")]
    [StringLength(300, MinimumLength = 3, ErrorMessage ="O {} deve ter no mínimo {} e no máximo {} caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição curta deve ser informado")]
    [Display(Name = "Descrição")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "O {} deve ter no mínimo {} e no máximo {} caracteres")]
    public string DescricaoCurta { get; set; }

    [Required(ErrorMessage = "A descrição detalhada deve ser informado")]
    [Display(Name = "Descrição detalhada")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "O {} deve ter no mínimo {} e no máximo {} caracteres")]
    public string DescricaoDetalhada { get; set; }

    [Required(ErrorMessage = "A descrição curta deve ser informado")]
    [Display(Name = "Preço")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
    public decimal Preco { get; set; }

    [Display(Name = "Imagem Url")]
    public string ImagemUrl { get; set; }

    [Display(Name = "Imagem ThumbnailUrl")]
    public string ImagemThumbnailUrl { get; set; }

    [Display(Name = "O lanche é favorito?")]
    public bool IsLanchePreferido { get; set; }

    [Display(Name = "Tem em estoque?")]
    public bool EmEstoque { get; set; }
    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }

}
