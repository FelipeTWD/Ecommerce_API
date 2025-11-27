using Domain.Helpers;
using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Ecommerce_API.Services;
using System.Security.Cryptography.X509Certificates;

namespace Ecommerce_API.Services;

public class PagamentoService 
{
    private readonly IPagamento _pagamentoRepository;
    public PagamentoService(IPagamento pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
    }

    public void SalvarPagamento(Pagamento pagamento)
    {
        try
        {
            if (pagamento == null || pagamento.Valor <= 0)
            {
                throw new ArgumentException("Pagamento inválido.");
            }
        }
        catch (ArgumentException ex)
        {
            throw new("Erro ao processar o Pagamento:" + ex.Message);
        }
        finally
        {
            _pagamentoRepository.SalvarPagamento(pagamento);
        }
    }
    public decimal PagamentoViaCartao(decimal valor, int Parcelas, DateTime Vencimento)
    {
        IPagamento pagamento = new PagamentoViaCartao
        {
            Valor = valor,
            Parcelas = Parcelas
        };
        Console.WriteLine("Pagamento via Cartão selecionado.");
        if ( Parcelas > 1)
        {
            pagamento.Valor += pagamento.Valor * 0.10m; // Adiciona 10% de taxa para 2 ou mais parcelas
        }
        else if (Parcelas == 1)
        {
            return pagamento.Valor; 
        }
    }
//pagamento.Valor;
}