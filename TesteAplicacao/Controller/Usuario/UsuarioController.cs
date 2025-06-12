using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.DTO;
using TesteAplicacao.Infraestrutra.Responses;
using TesteAplicacao.Services;
namespace TesteAplicacao.Controller.Usuario
{
    [Route("/usuario/")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        private readonly UsuarioService _usuarioService;

        public ProdutoController(UsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> InserirUsuario([FromBody] InserirUsuarioRequestDto request)
        {
            return Ok(new HttpOkResponse<bool>("Usuario inserido com sucesso.", await _usuarioService.InserirUsuario(request)));
        }

        [HttpPost("{usuario_id}/inativar")]
        public async Task<IActionResult> AtivarInativarUsuario(uint usuario_id)
        {
            return Ok(new HttpOkResponse<bool>("Usuario alterado com sucesso.", await _usuarioService.AtivarInativarUsuario(usuario_id)));
        }
        
        [HttpPut("{usuario_id}")]
        public async Task<IActionResult> AlterarUsuario([FromBody] AlterarUsuarioRequestDto request, uint usuario_id )
        {
            return Ok(new HttpOkResponse<bool>("Usuario Alterado com sucesso.", await _usuarioService.AlterarUsuario(request, usuario_id)));
        }
    }
}
