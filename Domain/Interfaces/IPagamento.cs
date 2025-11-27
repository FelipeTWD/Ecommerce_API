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
        decimal Valor { get; set; }

        bool SalvarPagamento(Pagamento pagamento);
    }
    public class PagamentoViaCartao : IPagamento
    {
        public DateTime Vencimento { get; set; } = DateTime.Now;
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }

        bool IPagamento.SalvarPagamento(Pagamento pagamento)
        {
          return true;
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
