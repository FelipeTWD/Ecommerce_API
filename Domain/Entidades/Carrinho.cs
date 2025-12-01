using System.Collections.Generic;
using System.Linq;

namespace Domain.Entidades;

public class Carrinho
{
    public int IdCarrinho { get; set; }
    public int ClienteId { get; set; }
    public List<ItemCarrinho> ListaItensCarrinho { get; set; } = new();
    public decimal Total = 0m;
    /*private decimal _total = 0m;
    public decimal Total
    {
        get
        {
            if (ListaItensCarrinho != null && ListaItensCarrinho.Any())
            {
                return ListaItensCarrinho.Sum(i => i.SubTotal);
            }
            return _total;
        }
        set
        {
            _total = value;
        }
    }*/
}
