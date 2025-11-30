using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        public void Cadastrar(Cliente cliente)
        {
            cliente.Id = BancoSql.ListaClientes.Any() ? BancoSql.ListaClientes.Max(c => c.Id) + 1 : 1;
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
}