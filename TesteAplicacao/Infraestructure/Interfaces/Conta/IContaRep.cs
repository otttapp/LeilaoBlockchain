using TesteAplicacao.DTO;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Interfaces
{
    public interface IContaRep : IRepositoryBase<Conta>
    {
        Task<PagedResult<GetContasDto>> GetContas(PaginacaoRequestDTO dto, bool ativo);
    }
}
