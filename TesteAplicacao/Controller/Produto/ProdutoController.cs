using Cuca_Api.Controller;
using Cuca_Api.Infraestrutra.Responses;
using Microsoft.AspNetCore.Mvc;
using PD_Api.Services;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;

namespace PD_Api.Controller
{
    [Route("/produto")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        private readonly ProdutoService _propdutoService;

        public ProdutoController(ProdutoService produtoService)
        {
            this._propdutoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> InserirProduto([FromBody] InserirProdutoRequestDto request)
        {
            return Ok(new HttpOkResponse<bool>("Usuario inserido com sucesso.", await _propdutoService.InserirProduto(request)));
        }
    }
}
