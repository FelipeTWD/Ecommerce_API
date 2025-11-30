using Application.DTOs;
using Domain.Entidades;
using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce_API.Services;

public class ClientesService
{
    private IClienteRepository _clienteRepository;
    public ClientesService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
    public void Cadastrar(ClienteDTO clienteDTO)
    {
        try
        {
            if (clienteDTO is null)
                throw new DomainException("Dados do cliente não podem ser nulos.");

            if (string.IsNullOrWhiteSpace(clienteDTO.Nome))
                throw new DomainException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDTO.Senha))
                throw new DomainException("A senha é obrigatória.");

            if (clienteDTO.EnderecoCliente is null)
                throw new DomainException("O endereço é obrigatório.");

            // Mapeia usando seu DTO
            Cliente cliente = clienteDTO.Mapear();

            // Chamada ao repositório
            _clienteRepository.Cadastrar(cliente);
        }
        catch (DomainException ex)
        {
            throw new Exception("Erro ao cadastrar cliente: " + ex.Message);
        }
    }
    public List<ClienteDTO> Listar()
    {
        try
        {
            List<Cliente> clientes = _clienteRepository.Listar();
            List<ClienteDTO> clientesDTO = new List<ClienteDTO>();
            foreach (Cliente cliente in clientes)
            {
 
                if (cliente == null)
                {
                    throw new DomainException("Cliente não encontrado.");
                }
                if (cliente.EnderecoCliente == null)
                {
                    throw new ArgumentException("Endereço do cliente não encontrado.");
                }

                ClienteDTO clienteDTO = new ClienteDTO
                {
                    Nome = cliente.Nome,
                    EnderecoCliente = cliente.EnderecoCliente,
                };
                clientesDTO.Add(clienteDTO);
                
            }
            return clientesDTO;
        }
        catch (DomainException ex)
        {
            throw new DomainException("Realmente não há o clientes: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Erro ao carregar o endereço do clientes: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao listar clientes: " + ex.Message);
        }
    }
    public ClienteDTO ObterClientePorId(int id)
    {
        try
        {// Buscar o cliente no repositório 
            Cliente cliente = _clienteRepository.ObterClientePorId(id);
            if (cliente == null)
            {
                throw new DomainException("Cliente não encontrado.");
            }
            ClienteDTO clienteDTO = new ClienteDTO
            {
                Nome = cliente.Nome,
                EnderecoCliente = cliente.EnderecoCliente
            };
            return clienteDTO;
        }

        catch (DomainException ex)
        {
            throw new DomainException("Cliente Inesistente: " + ex.Message);
        }
    }
}
