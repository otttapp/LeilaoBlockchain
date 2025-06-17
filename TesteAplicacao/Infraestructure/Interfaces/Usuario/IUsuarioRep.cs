using TesteAplicacao.DTO;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Interfaces
{
    public interface IUsuarioRep : IRepositoryBase<Usuario>
    {
        Task<Usuario> FindByEmail(string email);
        Task<Usuario?> GetByHashAsync(byte[] senhaHash);
        Task<PagedResult<GetUsuariosDto>> GetUsuarios(PaginacaoRequestDTO dto, bool ativo);
        Task<GetUsuariosDto?> GetUsuariosByID(uint usuario_id);
    }
}
