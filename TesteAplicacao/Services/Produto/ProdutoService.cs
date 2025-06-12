using System.Text;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Repository;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Services
{
    public class ProdutoService
    {
        private readonly MainDBContext _dbContext;
        private readonly IProdutoRep _produtoRep;
        public ProdutoService(MainDBContext dbContext, IProdutoRep produtoRep)
        {
            this._dbContext = dbContext;
            this._produtoRep = produtoRep;
        }

        public async Task<bool> InserirProduto(InserirProdutoRequestDto request)
        {
            var produto = new Produto
            {
               nome = request.nome,
               ativo = request.ativo,
               descricao = request.descricao,
               datahora_insercao = DateTime.UtcNow,
               valor = request.valor,
               raridade = request.raridade
            };

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _produtoRep.Add(produto);
            });

            return true;
        }
    }
}
