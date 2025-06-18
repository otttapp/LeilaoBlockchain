using System.Text;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Repository;
using TesteAplicacao.DTO;
using TesteAplicacao.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Services
{
    public class ProdutoService
    {
        private readonly MainDBContext _dbContext;
        private readonly IProdutoRep _produtoRep;
        private readonly IUsuarioRep _usuarioRep;
        private readonly UsuarioAutenticadoService _usuarioAutenticado;

        public ProdutoService(MainDBContext dbContext, IProdutoRep produtoRep, IUsuarioRep usuarioRep, UsuarioAutenticadoService usuarioAutenticado)
        {
            this._dbContext = dbContext;
            this._produtoRep = produtoRep;
            this._usuarioRep = usuarioRep;
            this._usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<bool> InserirProduto(InserirProdutoRequestDto request)
        {
            var usuarioLogado = await _usuarioAutenticado.GetUsuarioLogado();

            var produto = new Produto
            {
               nome = request.nome,
               ativo = request.ativo,       
               descricao = request.descricao,
               datahora_insercao = DateTime.UtcNow,
               valor = request.valor,
               raridade = request.raridade,      
               usuario_id = usuarioLogado!.usuario_id
            };

            if (produto.valor <= 0)
            {
                throw new BusinessException("Você não pode inserir um produto com valor negativo");
            }

            if (string.IsNullOrWhiteSpace(produto.nome))
            {
                throw new BusinessException("O nome não pode ser vazia");
            }

            if (string.IsNullOrEmpty(produto.descricao))
            {
                throw new BusinessException("A descrição não pode ser vazia");
            }

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _produtoRep.Add(produto);
            });

            return true;
        }

        public async Task<bool> AlterarProduto(AlterarProdutoRequestDto request, uint produto_id)
        {
            var produto = await _produtoRep.GetByIdThrowsIfNull(produto_id);

            produto.nome = request.nome;
            produto.descricao = request.descricao;
            produto.valor = request.valor;
            produto.raridade = request.raridade;

            if (produto.valor <= 0)
            {
                throw new BusinessException("Você não pode alterar um produto para um valor negativo");
            }

            if (string.IsNullOrWhiteSpace(produto.nome))
            {
                throw new BusinessException("O nome não pode ser vazia");
            }

            if (string.IsNullOrEmpty(produto.descricao))
            {
                throw new BusinessException("A descrição não pode ser vazia");
            }

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _produtoRep.Update(produto);
                await _dbContext.SaveChangesAsync(); 
            });

            return true;
        }

        public async Task<bool> InativarProduto(uint produto_id)
        {
            var produto = await _produtoRep.GetByIdThrowsIfNull(produto_id);

            produto.ativo = !produto.ativo;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _produtoRep.Update(produto);
            });
            
            return true;
        }

        public async Task<PagedResult<GetPodutosDto>> GetProdutos(PaginacaoRequestDTO dto, bool ativo)
        {
            return await _produtoRep.GetProdutos(dto, ativo);
        }
        public async Task<GetPodutosDto?> GetProdutosByID(uint produto_id)
        {
            return await _produtoRep.GetProdutosByID(produto_id);
        }

        public async Task<List<GetPodutosDto>> GetMeusPrdutos()
        {
            var usuario_id = await _usuarioAutenticado.GetUsuarioLogadoId();

            return await _produtoRep.GetMeusPrdutos(usuario_id);
        }
    }
}
