using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.DTO;
using TesteAplicacao.Infraestrutra.Responses;
using TesteAplicacao.Services;

namespace TesteAplicacao.Controller.Conta
{
    [Route("/conta")]
    [ApiController]
    public class ContaController : BaseController
    {
        private readonly ContaService _contaService;

        public ContaController(ContaService contaService)
        {
            this._contaService = contaService;
        }

        [HttpPost("{usuario_id}")]
        public async Task<IActionResult> InserirConta([FromBody] InserirContaRequestDto request, uint usuario_id)
        {
            return Ok(new HttpOkResponse<bool>("Conta inserida com sucesso.", await _contaService.InserirConta(request, usuario_id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetContas([FromQuery] PaginacaoRequestDTO dto/*, [FromQuery] bool ativo*/)
        {
            return Ok(new HttpOkResponse<PagedResult<GetContasDto>>("Contas listados com sucesso!", await _contaService.GetContas(dto/*, ativo*/)));
        }
    }
}
