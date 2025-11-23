using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPagamento
    {
        DateTime Vencimento { get; set; }
        decimal Valor { get; set; }

        void SalvarPagamento(Pagamento pagamento);
    }
    public class PagamentoViaCartao : IPagamento
    {
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }

        void IPagamento.SalvarPagamento(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }
    }
    public class PagamentoViaPix : IPagamento
    {
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }

        void IPagamento.SalvarPagamento(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }
    }
    public class PagamentoViaBoleto : IPagamento
    {
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        void IPagamento.SalvarPagamento(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }
    }
}
