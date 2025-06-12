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
            var usuario = new Usuario
            {
                nome = request.nome,
                senha = request.senha,
                email = request.email,
                telefone = request.telefone,
                ativo = true,
                datahora_insercao = DateTime.UtcNow,

            };
           

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Add(usuario);
            });

            return true;
        }

        public async Task<bool> AlterarUsuario(AlterarUsuarioRequestDto request, uint usuario_id)
        {
            var usuario = await _usuarioRep.GetById(usuario_id);

            if (usuario is null)
            {
                throw new BusinessException("Usuário não encontrado.");
            }

             usuario = new Usuario
            {
                nome = request.nome,
                senha = request.senha,
                email = request.email,
                telefone = request.telefone
            };

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }

        public async Task<bool> AtivarInativarUsuario(uint usuario_id)
        {
            var usuario = await _usuarioRep.GetById(usuario_id);

            if (usuario is null)
            {
                throw new BusinessException("Usuário não encontrado.");
            }

           usuario.ativo = !usuario.ativo;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }
    }
}
