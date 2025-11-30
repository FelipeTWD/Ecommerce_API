using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositorios;

public class ClienteRepository : IClienteRepository
{
    public void Cadastrar(Cliente cliente)
    {
        using var conn = new DbConnection();
        string query = @"INSERT INTO public.Cliente(
	                Nome, Senha, Endereco)
	                VALUES (@Nome, @Senha, @Endereco);";
        var result = conn.Connection.Execute(query, new
        {
            cliente.Nome,
            cliente.Senha,
            cliente.Endereco
        });
        BancoSql.ListaClientes.Add(cliente);
    }

    public Cliente ObterClientePorId(int id)
    {
        return BancoSql.ListaClientes.FirstOrDefault(c => c.Id == id)!;
    }

    public List<Cliente> Listar()
    {
       return BancoSql.ListaClientes.ToList();
    }

    public void Remover(int id)
    {
      Cliente? cliente = BancoSql.ListaClientes.FirstOrDefault(c => c.Id == id);
      if (cliente != null) BancoSql.ListaClientes.Remove(cliente);
    }
    
}