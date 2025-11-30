using Ecommerce_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PagamentosController : ControllerBase
{
    private PagamentoService _pagamentoService;

    public PagamentosController(PagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    //[HttpPost("Pagar")]
    //[HttpGet("ListarParcelas")]
}
