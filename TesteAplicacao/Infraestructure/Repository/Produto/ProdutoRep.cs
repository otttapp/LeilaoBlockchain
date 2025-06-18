using Azure;
using Microsoft.EntityFrameworkCore;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Extensions;
using TesteAplicacao.Infraestructure.Interfaces;
using TesteAplicacao.Infraestructure.Repository;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class ProdutoRep : RepositoryBase<Produto>, IProdutoRep
    {

        public ProdutoRep(MainDBContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public async Task<PagedResult<GetPodutosDto>> GetProdutos(PaginacaoRequestDTO dto, bool ativo)
        {
            var produtos = await db.Produtos
                .Include(u => u.usuario)
                .Where(p => p.ativo == ativo
                    && (EF.Functions.Like(p.nome, dto.filtro_generico) 
                    || (String.IsNullOrWhiteSpace(dto.filtro_generico)
                    || EF.Functions.Like((string)p.descricao!, dto.filtro_generico))
                     ))
                .Select(p => new GetPodutosDto
                {
                    produto_id = p.produto_id,
                    descricao = p.descricao ?? string.Empty,
                    nome = p.nome ?? string.Empty,
                    ativo = p.ativo,
                    data_compra = p.data_compra,
                    datahora_insercao = p.datahora_insercao,
                    valor = p.valor,
                    raridade = p.raridade,
                    usuario = new UsuarioBaseDto()
                    {
                        usuario_id = p.usuario.usuario_id,
                        nome = p.usuario.nome,
                    }
                })
                .PaginateAsync(dto);

            return produtos.Result.Any() ? produtos : PagedResult<GetPodutosDto>.Empty;
        }

        public async Task<GetPodutosDto?> GetProdutosByID(uint produto_id)
        {
            return await db.Produtos
                .Include(u => u.usuario)
                .Where(p => p.produto_id == produto_id)
                .Select(p => new GetPodutosDto
                {
                    produto_id = p.produto_id,
                    descricao = p.descricao ?? string.Empty,
                    nome = p.nome ?? string.Empty,
                    ativo = p.ativo,
                    data_compra = p.data_compra,
                    datahora_insercao = p.datahora_insercao,
                    valor = p.valor,
                    raridade = p.raridade,
                    usuario = new UsuarioBaseDto()
                    {
                        usuario_id = p.usuario.usuario_id,
                        nome = p.usuario.nome,
                    }
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<GetPodutosDto>> GetMeusPrdutos(uint usuario_id)
        {
            return await db.Produtos
                .Include(u => u.usuario)
                .Where(p => p.usuario.usuario_id == usuario_id)
                .Select(p => new GetPodutosDto
                {
                    produto_id = p.produto_id,
                    descricao = p.descricao ?? string.Empty,
                    nome = p.nome ?? string.Empty,
                    ativo = p.ativo,
                    data_compra = p.data_compra,
                    datahora_insercao = p.datahora_insercao,
                    valor = p.valor,
                    raridade = p.raridade,
                    usuario = new UsuarioBaseDto()
                    {
                        usuario_id = p.usuario.usuario_id,
                        nome = p.usuario.nome,
                    }
                })
                .ToListAsync();
        }
    }
}

