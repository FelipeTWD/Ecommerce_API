using Application;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Services;
using Domain.Helpers;

namespace Ecommerce_API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PedidosController : ControllerBase
{
    private PedidosService _pedidosService;
    public PedidosController(PedidosService pedidosService)
    {
        _pedidosService = pedidosService;
    }
    [HttpPost("incluir")]
    public ActionResult Incluir(PedidoDTO pedidoDTO)
    {
        try
        {
                _pedidosService.Incluir(pedidoDTO);
            return Ok("O pedido foi incluido com sucesso.");

        }
        catch(DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    public ActionResult Listar()
    {
        try
        {
            var listaPedidos = _pedidosService.Listar();
            return Ok(listaPedidos);

        }
        catch (DomainException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }
    [HttpDelete("{id}")]
    public ActionResult Remover(int id)
    {
        try
        {
            _pedidosService.Remover(id);
            return Ok("O pedido foi removido com sucesso.");

        }
        catch (DomainException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno do servidor: " + ex.Message);
        }
    }
}
