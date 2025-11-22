using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;

namespace Application.DTOs;

public class PedidosService
{
    private IPedidoRepository _pedidoRepository;
    public PedidosService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }
    public void Incluir(PedidoDTO pedidoDTO)
    {
        Pedido pedido = pedidoDTO.Mapear();
        _pedidoRepository.Incluir(pedido);
    }
    public List<PedidoDTO> Listar()
    {
        List<Pedido> listaPedidos = _pedidoRepository.Listar();
        List<PedidoDTO> listaPedidosDTO = new List<PedidoDTO>();
        foreach (Pedido pedido in listaPedidos)
        {
            PedidoDTO dto = new PedidoDTO
            {
                IdPedido = pedido.IdPedido,
                ClienteId = pedido.ClienteId,
                ListaItensPedido = pedido.ListaItensPedido.Select(item => new PedidoItensDTO
                {
                    IdItem = item.IdPedido,
                    ProdutoId = item.IdProduto,
                    Quantidade = item.Quantidade
                }).ToList()
            };
            listaPedidosDTO.Add(dto);
        }
        return listaPedidosDTO;
    }
    public void Remover(int id)
    {
        _pedidoRepository.Remover(id);
    }
}
