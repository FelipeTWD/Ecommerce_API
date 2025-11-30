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
    public decimal PagamentoViaCartao(decimal valor, int Parcelas, DateTime Vencimento, bool SalvarPagamento)
    {
        IPagamento pagamento = new PagamentoViaCartao
        {
            Valor = valor,
            Parcelas = Parcelas,
            Vencimento = Vencimento
        };
        try
        {

        
            Console.WriteLine("Pagamento via Cartão selecionado.");
            if  (Vencimento < DateTime.Now)
            {
                Console.WriteLine("O pagamento expirou.");
            }
            Console.WriteLine("Escolha em quantas parcelas será feito o pagamento 1x-12x. Tendo juros a partir de 2x.");
            Parcelas = Console.ReadLine() !=null ? 1 : int.Parse(Console.ReadLine());
            if (Parcelas > 1 && Parcelas <= 12)
            {
                Console.WriteLine($"Dessa forma o pagamento será feito em {Parcelas} parcelas, com 10% de juros.");
                return pagamento.Valor += pagamento.Valor * 0.10m; // Adiciona 10% de taxa para 2 ou mais parcelas
            }
            else if (Parcelas == 1)
            {
                Console.WriteLine($"Dessa forma o pagamento será feito em {Parcelas} parcelas e não vai ter juros.");
                return pagamento.Valor;
            }
            else
            {
                throw new ArgumentException("Número de parcelas inválido.");
            }
                
        }
        catch(ArgumentException ex)
        {
            throw new ArgumentException("Impossivel completar o pagamento." + ex.Message);
        }
        finally
        {
            _pagamentoRepository.SalvarPagamento((Pagamento)pagamento);

        }
    }
    public decimal PagamentoViaPix(decimal desconto, decimal valor, DateTime Vencimento, bool SalvarPagamento) 
    {
        IPagamento pagamento = new PagamentoViaPix
        {
            Desconto = desconto,
            Valor = valor,
            Vencimento = Vencimento
        };
        try
        {
            Console.WriteLine("Pagamento via Pix selecionado.");
            if (Vencimento <= DateTime.Now.AddMinutes(30))
            {
                return pagamento.Valor -= pagamento.Valor * 0.10m; //Desconto de 10%.
            }
            else
            {
                throw new ArgumentException("Pagamento indisponivel.");
            }
        }
        catch(ArgumentException ex)
        {
            throw new ArgumentException("Por favor, tente outra vez." + ex.Message);
        }

    }
    public decimal PagamentoViaBoleto(decimal valor, DateTime Vencimento, bool SalvarPagamento)
    {
        IPagamento pagamento = new PagamentoViaBoleto
        {
            Valor = valor,
            Vencimento = Vencimento
        };

        Console.WriteLine("Pagamento via Boleto selecionado.");
        if (Vencimento > DateTime.Now.AddDays(3))
        {
            Console.WriteLine("Boleto expirou.");
            return 0m;
        }
        else
        {
            Console.WriteLine("Boleto disponivel para pagamento.");
            _pagamentoRepository.SalvarPagamento((Pagamento)pagamento);
            return pagamento.Valor;
        }
    }
}