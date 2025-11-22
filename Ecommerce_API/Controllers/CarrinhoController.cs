using Application;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Services;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private CarrinhoService _carrinhoService;
        public CarrinhoController(CarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }
        [HttpGet("Listar")]
        public ActionResult Listar()
        {
            try
            {
                var carrinhos = _carrinhoService.Listar();
                if (carrinhos == null || carrinhos.count == 0)
                    return NotFound("Nenhum carrinho encontrado.");
                return Ok(carrinhos);
            }
            catch (DomainExcpetion ex)
            { //Erro precissível de domínio
                return BadRequest(ex.Message);
            }
            catch (ArgumentExcpetion ex)
            { //Erro precissível de argumento inválido
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            { //Erro não precissível
                return StatusCode(500, "Erro interno do servidor.");

            }
        }
            [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {             _carrinhoService.Remover(id);
            return Ok();
        }
        [HttpGet("total")]
        public ActionResult CalcularTotal()
        {
            decimal total = _carrinhoService.CalcularTotal();
            return Ok(total);
        }
    }
}
