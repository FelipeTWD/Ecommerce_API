using System.Runtime.InteropServices;

namespace Domain.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public Endereco EnderecoCliente { get; set; } = new();

    public string AdicionarSenha(string senha)
    {
        Console.WriteLine("Digite a senha do cliente:");
        senha = Console.ReadLine();
        return senha;
    }
}
