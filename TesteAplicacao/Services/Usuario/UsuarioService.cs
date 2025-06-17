using Projeto_Aplicado_II_API.Infrastructure.Extensions;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Services
{
    public class UsuarioService
    {
        private readonly MainDBContext _dbContext;
        private readonly IProdutoRep _produtoRep;
        private readonly IUsuarioRep _usuarioRep;
        
        
        public UsuarioService(MainDBContext dbContext, IProdutoRep produtoRep, IUsuarioRep usuarioRep)
        {
            this._dbContext = dbContext;
            this._produtoRep = produtoRep;
            this._usuarioRep = usuarioRep;
        }

        public async Task<bool> InserirUsuario(InserirUsuarioRequestDto request)
        {
            (byte[] senhaHash, byte[] saltHash) = request.senha.HashPassword();

            var usuario = new Usuario
            {
                nome = request.nome,
                email = request.email,
                telefone = request.telefone,
                ativo = true,
                datahora_insercao = DateTime.UtcNow,
                senha_hash = senhaHash,
                senha_salt = saltHash
            };

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Add(usuario);
            });

            return true;
        }

        public async Task<bool> AlterarUsuario(AlterarUsuarioRequestDto request, uint usuario_id)
        {
            var usuario = await _usuarioRep.GetByIdThrowsIfNull(usuario_id);

            if (!usuario.ativo)
            {
                throw new BusinessException("Você não pode alterar um usuário que está inativo, antes disso deixe-o ativo");
            }

            usuario.nome = request.nome;
            usuario.email = request.email;
            usuario.telefone = request.telefone;
            
            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }

        public async Task<bool> AlterarSenha(AlterarSenhaUsuarioRequestDto request, uint usuario_id)
        {
            var usuario = await _usuarioRep.GetByIdThrowsIfNull(usuario_id);

            if (!usuario.ativo)
            {
                throw new BusinessException("Você não pode alterar um usuário inativo. Ative-o primeiro.");
            }

            var senhaCorreta = request.SenhaAtual.VerifyPassword(usuario.senha_hash, usuario.senha_salt);
            if (!senhaCorreta)
            {
                throw new BusinessException("Senha atual incorreta.");
            }

            (byte[] novaSenhaHash, byte[] novoSalt) = request.NovaSenha.HashPassword();

            usuario.senha_hash = novaSenhaHash;
            usuario.senha_salt = novoSalt;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }

        public async Task<bool> AtivarInativarUsuario(uint usuario_id)
        {
            var usuario = await _usuarioRep.GetByIdThrowsIfNull(usuario_id);

           usuario.ativo = !usuario.ativo;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }

        public async Task<UsuarioResponseDto> Login(LoginRequestDto dtoUsuario)
        {
            var usuario = await _usuarioRep
                .FindByEmail(dtoUsuario.email)
                ?? throw new BusinessException("USUARIO_SENHA_INVALIDOS", "Usuário ou senha inválidos.");

            if (!usuario.ativo)
                throw new BusinessException("USUARIO_INATIVO", "Usuário não está ativo!");

            bool senhaOK = dtoUsuario.password.VerifyPassword(usuario.senha_hash, usuario.senha_salt);
            if (!senhaOK)
                throw new BusinessException("USUARIO_SENHA_INVALIDOS", "Usuário ou senha inválidos.");

            string token = Convert.ToBase64String(usuario.senha_hash);

            return new UsuarioResponseDto
            {
                token = token,
                email = dtoUsuario.email
            };
        }

        public async Task<PagedResult<GetUsuariosDto>> GetUsuarios(PaginacaoRequestDTO dto, bool ativo)
        {
           return await _usuarioRep.GetUsuarios(dto, ativo);
        }
        public async Task<GetUsuariosDto?> GetUsuariosByID(uint usuario_id)
        {
            return await _usuarioRep.GetUsuariosByID(usuario_id);
        }

    }
}
