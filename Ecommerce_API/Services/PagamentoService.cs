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
    public void ProcessarPagamento(PagamentoDTO pagamentoDTO)
    {
        try
        {
            // Mapear o DTO para a entidade Pagamento
            Pagamento pagamento = pagamentoDTO.Mapear();
            if (pagamento == null || pagamento.Valor <= 0)
            {
                throw new ArgumentException("Pagamento inválido.");
            }
            // Salvar o pagamento no repositório
            _pagamentoRepository.SalvarPagamento(pagamento);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public decimal PagamentoViaBoleto(decimal valor)
    {
        try
        {

            if (_pagamentoRepository.GetType().Name != "PagamentoViaBoleto")
            {
                throw new InvalidOperationException("O repositório de pagamento não suporta pagamento via boleto.");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("Valor do pagamento inválido.");
            }
            // Lógica específica para pagamento via boleto
            decimal taxaBoleto = 2.5m; // Exemplo de taxa fixa
            return valor + taxaBoleto;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public decimal PagamentoViaPix(decimal valor)
    {
        try 
        { 

            if (_pagamentoRepository.GetType().Name != "PagamentoViaPix")
            {
                throw new InvalidOperationException("O repositório de pagamento não suporta pagamento via Pix.");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("Valor do pagamento inválido.");
            }
            // Lógica específica para pagamento via Pix
            decimal descontoPix = 1.0m; // Exemplo de desconto fixo
            return valor - descontoPix;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public decimal PagamentoViaCartao(decimal valor)
    {
        try
        {
            if (_pagamentoRepository.GetType().Name != "PagamentoViaCartao")
            {
                throw new InvalidOperationException("O repositório de pagamento não suporta pagamento via cartão.");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("Valor do pagamento inválido.");
            }
            // Lógica específica para pagamento via cartão
            decimal taxaCartao = valor * 0.03m; // Exemplo de taxa percentual
            return valor + taxaCartao;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
