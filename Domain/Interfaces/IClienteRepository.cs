using Domain.Entidades;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    bool Cadastrar(Cliente cliente);
    List<Cliente> Listar();
    Cliente ObterClientePorId(int id);
}
