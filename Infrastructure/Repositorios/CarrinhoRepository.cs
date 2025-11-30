using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorios
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        public void Remover(int id)
        {
            var carrinho = BancoSql.ListaCarrinhos.FirstOrDefault(c => c.IdCarrinho == id);
            if (carrinho != null) BancoSql.ListaCarrinhos.Remove(carrinho);
        }

        public List<Carrinho> Listar()
        {
            return BancoSql.ListaCarrinhos.ToList();
        }

        public decimal CalcularTotal(int IdCarrinho)
        {
            var c = BancoSql.ListaCarrinhos.FirstOrDefault(x => x.IdCarrinho == IdCarrinho);
            return c?.CalcularTotal() ?? 0m;
        }
    }
}
