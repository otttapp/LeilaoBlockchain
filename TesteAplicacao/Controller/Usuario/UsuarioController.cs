using Azure.Core;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
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

        [HttpPost("{usuario_id}/AlterarSenha")]
        public async Task<IActionResult> AlterarSenha( [FromBody] AlterarSenhaUsuarioRequestDto request, uint usuario_id)
        {
            return Ok(new HttpOkResponse<bool>("Senha alterado com sucesso.", await _usuarioService.AlterarSenha(request, usuario_id)));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            return Ok(new HttpOkResponse<UsuarioResponseDto>("Login efetuado com sucesso!", await _usuarioService.Login(request)));
        }

        [HttpGet]   
        public async Task<IActionResult> GetUsuarios([FromQuery] PaginacaoRequestDTO dto, [FromQuery] bool ativo )
        {
            return Ok(new HttpOkResponse<PagedResult<GetUsuariosDto>>("Usuários listados com sucesso!", await _usuarioService.GetUsuarios(dto, ativo)));
        }

        [HttpGet("{usuario_id}")]
        public async Task<IActionResult> GetUsuariosByID(uint usuario_id)
        {
            return Ok(new HttpOkResponse<GetUsuariosDto?>("Usuário listado com sucesso!", await _usuarioService.GetUsuariosByID(usuario_id)));
        }
    }
}
