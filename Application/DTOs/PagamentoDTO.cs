using Domain.Entidades;
namespace Application.DTOs;

public class PagamentoDTO
{

    public decimal Valor { get; set; }
    public int IdPagamento { get; set; }
    public Pagamento Mapear()
    {
        return new Pagamento
        {
            IdPagamento = this.IdPagamento,
            Valor = this.Valor,
        };
    }
}
    