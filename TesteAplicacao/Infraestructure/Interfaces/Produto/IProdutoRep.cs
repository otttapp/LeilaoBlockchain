using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Interfaces
{
    public interface IProdutoRep : IRepositoryBase<Produto>
    {
        Task<PagedResult<GetPodutosDto>> GetProdutos(PaginacaoRequestDTO dto, bool ativo);
        Task<GetPodutosDto?> GetProdutosByID(uint produto_id);
        Task<List<GetPodutosDto>> GetMeusPrdutos(uint usuario_id);
    }
}
