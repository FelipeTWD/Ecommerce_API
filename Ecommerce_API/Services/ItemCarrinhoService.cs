using Application.DTOs;

namespace Ecommerce_API.Services
{
    public class ItemCarrinhoService
    {
        public decimal ObterSubTotal(ItemCarrinhoDTO dto)
        {
            try
            {
                if (dto.Quantidade <0)
                    throw new ArgumentException("Quantidade não pode ser negativa.");
                var Entidade = dto.Mapear();
                return Entidade.IprimirSubTotal();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Erro ao calcular o subtotal: " + ex.Message);
            }
        }
    }
}
