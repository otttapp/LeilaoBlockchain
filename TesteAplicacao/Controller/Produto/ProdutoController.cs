using TesteAplicacao.Controller;
using TesteAplicacao.Infraestrutra.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.Services;
using TesteAplicacao.DTO;

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
            return Ok(new HttpOkResponse<bool>("Produto inserido com sucesso.", await _propdutoService.InserirProduto(request)));
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos([FromQuery] PaginacaoRequestDTO dto, [FromQuery] bool ativo)
        {
            return Ok(new HttpOkResponse<PagedResult<GetPodutosDto>>("Produtos listados com sucesso!", await _propdutoService.GetProdutos(dto, ativo)));
        }

        [HttpGet("{produto_id}")]
        public async Task<IActionResult> GetProdutosByID(uint produto_id)
        {
            return Ok(new HttpOkResponse<GetPodutosDto?>("Produto listado com sucesso!", await _propdutoService.GetProdutosByID(produto_id)));
        }

        [HttpGet("meus")]
        public async Task<IActionResult> GetMeusPrdutos()
        {
            return Ok(new HttpOkResponse<List<GetPodutosDto>>("Seus produtos:", await _propdutoService.GetMeusPrdutos()));
        }

        [HttpPost("{produto_id}/Inativar")]
        public async Task<IActionResult> InativarProduto(uint produto_id)
        {
            return Ok(new HttpOkResponse<bool>("Produto Inativado com sucesso.", await _propdutoService.InativarProduto(produto_id)));
        }

        [HttpPut("{produto_id}")]
        public async Task<IActionResult> AlterarProduto([FromBody] AlterarProdutoRequestDto request, uint produto_id)
        {
            return Ok(new HttpOkResponse<bool>("Produto alterado com sucesso.", await _propdutoService.AlterarProduto(request, produto_id)));
        }

    }
}
