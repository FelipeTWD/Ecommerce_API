using Domain.Entidades;
namespace Application.DTOs;

public class PagamentoDTO
{

    public decimal Valor { get; set; }
    public Pagamento Mapear()
    {
        return new Pagamento
        {
            Valor = this.Valor
        };
    }
}
    