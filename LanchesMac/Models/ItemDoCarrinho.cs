using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models;

public class ItemDoCarrinho
{
    public int ItemDoCarrinhoId { get; set; }
    public Lanche Lanche { get; set; } // Transforma em uma chave estrangeira na tabela, porque já é uma  entidade no meu negocio de dominio
    public int Quantidade { get; set; } 

    [StringLength(200)]
    public string CarrinhoCompraId { get; set; }
}
