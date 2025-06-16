using Microsoft.EntityFrameworkCore;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
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

    }
}
