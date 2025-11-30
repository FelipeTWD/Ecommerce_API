using System;
using Domain.Entidades;

namespace Application.DTOs
{
    public class ItemCarrinhoDTO
    {
        public int IdItemCarrinho { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get;  set; }
        // Propriedade calculada — não deve ser enviada pelo cliente como fonte de verdade
        public decimal SubTotal {  get; set; } // Calculado na entidade

        public ItemCarrinho Mapear()
        {
            return new ItemCarrinho
            {
                IdItemCarrinho = this.IdItemCarrinho,
                IdProduto = this.IdProduto,
                Quantidade = this.Quantidade,
                Preco = this.Preco
            };
        }
    }
}
