using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "O nome da categoria tem que ser informado")]
    [Display(Name ="Nome da Categoria")]
    [StringLength(200, MinimumLength = 3, ErrorMessage ="O {} deve ter no mínimo {} e no máximo {} caracteres")]
    public string CategoriaNome { get; set; }

    [Required(ErrorMessage = "Uma descrição sobre a categoria deve ser informada")]
    [Display(Name = "Nome da Categoria")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O {} deve ter no mínimo {} e no máximo {} caracteres")]
    public string Descricao { get; set; }
    public List<Lanche> Lanches { get; set;}

}
