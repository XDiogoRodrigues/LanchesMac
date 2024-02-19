using LanchesMac.Context;
using LanchesMac.Migrations;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string CarrinhoCompraId { get; set; }
    public List<ItemDoCarrinho> CarrinhoCompraItems { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //define uma sessão
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        //obtem um serviço do tipo do nosso contexto
        var context = services.GetService<AppDbContext>();

        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);

        //retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId,
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.ItensDoCarrinho.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

        if(carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new ItemDoCarrinho
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.ItensDoCarrinho.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public void RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.ItensDoCarrinho.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);


        if(carrinhoCompraItem == null)
        {
            if(carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;            
            }
            else
            {
                _context.ItensDoCarrinho.Remove(carrinhoCompraItem);
            }
        }
        _context.SaveChanges();
    }

    public List<ItemDoCarrinho> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                    _context.ItensDoCarrinho
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Lanche)
                    .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.ItensDoCarrinho.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

        _context.ItensDoCarrinho.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.ItensDoCarrinho
                     .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                     .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
        return total;

    }

}
