using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Interfaces
{
    public interface IUsuarioRep : IRepositoryBase<Usuario>
    {
        Task<Usuario> FindByEmail(string email);
        Task<Usuario?> GetByHashAsync(byte[] senhaHash);

    }
}
