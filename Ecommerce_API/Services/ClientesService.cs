using Application.DTOs;
using Domain.Entidades;
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
        List<Cliente> listaClientes = _clienteRepository.Listar();
        List<ClienteDTO> listaClientesDTO = new List<ClienteDTO>();
        foreach (Cliente cliente in listaClientes)
        {
            ClienteDTO dto = new ClienteDTO { Id = cliente.Id, Nome = cliente.Nome, Endereco = cliente.Endereco };
            listaClientesDTO.Add(dto);
        }
        return listaClientesDTO;
    }

}
