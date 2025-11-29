using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Ecommerce_API.Services;
using System.Security.Cryptography.X509Certificates;
using Domain.Helpers;
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
            Parcelas = Parcelas,
            Vencimento = Vencimento
        };

        Console.WriteLine("Pagamento via Cartão selecionado.");
        if  (Vencimento < DateTime.Now)
        {
            Console.WriteLine("O pagamento expirou.");
        }
        Console.WriteLine("Escolha em quantas parcelas será feito o pagamento 1x-12x. Tendo juros a partir de 2x.");
        Parcelas = Console.ReadLine() !=null ? 1 : int.Parse(Console.ReadLine());
        if ( Parcelas > 1 && Parcelas <= 12)
        {
            Console.WriteLine($"Dessa forma o pagamento será feito em {Parcelas} parcelas.");
            return pagamento.Valor += pagamento.Valor * 0.10m; // Adiciona 10% de taxa para 2 ou mais parcelas
        }
        else if (Parcelas == 1)
        {
            Console.WriteLine($"Dessa forma o pagamento será feito em {Parcelas} parcelas.");
            return pagamento.Valor; 
        }
    }
//pagamento.Valor;
}