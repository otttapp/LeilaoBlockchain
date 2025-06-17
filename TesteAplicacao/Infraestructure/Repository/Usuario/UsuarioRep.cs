using Microsoft.EntityFrameworkCore;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Extensions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class UsuarioRep : RepositoryBase<Usuario>, IUsuarioRep
    {
        public UsuarioRep(MainDBContext repositoryContext) : base(repositoryContext)
        {
        }

        public override Task Add(Usuario entity)
        {
            if (db.Usuarios.Where(u => u.email == entity.email).Any())
                throw new BusinessException("EMAIL_JA_CADASTRADO", "Já existe um usuário cadastrado para este email");

            return base.Add(entity);
        }

        public async Task<Usuario?> FindByEmail(string email)
        {
            return await db.Usuarios.Where(u => u.email == email).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> GetByHashAsync(byte[] senhaHash)
        {
            return await db.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.senha_hash == senhaHash);
        }

        public async Task<PagedResult<GetUsuariosDto>> GetUsuarios(PaginacaoRequestDTO dto, bool ativo)
        {
            var usuarios = await db.Usuarios
                .Where(u => u.ativo == ativo
                    && (EF.Functions.Like(u.nome, dto.filtro_generico) || (String.IsNullOrWhiteSpace(dto.filtro_generico)
                    || EF.Functions.Like((string)u.email!, dto.filtro_generico) || EF.Functions.Like(u.telefone, dto.filtro_generico)
                     )))
                .Select(u => new GetUsuariosDto
                {
                    usuario_id = u.usuario_id,
                    nome = u.nome,
                    email = u.email ?? string.Empty,
                    ativo = u.ativo,
                    datahora_insercao = u.datahora_insercao,
                    datahora_desativacao = u.datahora_desativacao,
                })
                .PaginateAsync(dto);

            return usuarios.Result.Any() ? usuarios : PagedResult<GetUsuariosDto>.Empty;

        }
    }
}
