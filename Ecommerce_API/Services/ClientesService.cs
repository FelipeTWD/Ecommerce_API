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
        Cliente cliente = clienteDTO.Mapear();
        _clienteRepository.Cadastrar(cliente);
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
                if (cliente.Endereco == null)
                {
                    throw new ArgumentException("Endereço do cliente não encontrado.");
                }

                ClienteDTO clienteDTO = new ClienteDTO
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Endereco = cliente.Endereco
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
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco
            };
            return clienteDTO;
        }

        catch (DomainException ex)
        {
            throw new DomainException("Cliente Inesistente: " + ex.Message);
        }
    }
}
