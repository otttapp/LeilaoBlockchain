using TesteAplicacao.Controller;
using TesteAplicacao.Infraestrutra.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.Services;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Controller
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
