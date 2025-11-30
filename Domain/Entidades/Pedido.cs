namespace Domain.Entidades;
public class Pedido
{
    public int IdPedido { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public List<PedidoItens> ListaItensPedido { get; set; } = new();
}
