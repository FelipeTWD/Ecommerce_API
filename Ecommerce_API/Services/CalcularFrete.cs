using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Ecommerce_API.Services;


namespace Ecommerce_API.Services;

public class CalcularFrete
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IFrete _freteService;
    public CalcularFrete(IClienteRepository clienteRepository, IFrete freteService)
    {
        _clienteRepository = clienteRepository;
        _freteService = freteService;
    }
    public decimal Calcular(FreteDTO freteDTO)
    {
        // Mapear o DTO para a entidade Cliente
        Cliente cliente = freteDTO.Mapear();
        // Cadastrar o cliente (opcional, dependendo da lógica do negócio)
        _clienteRepository.Cadastrar(cliente);
        // Calcular o frete usando o serviço de frete injetado
        decimal valorFrete = _freteService.CalcularFrete(cliente);
        return valorFrete;
    }
}
