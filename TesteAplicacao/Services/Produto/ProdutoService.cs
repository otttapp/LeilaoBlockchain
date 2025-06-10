using System.Text;
using Cuca_Api.Infraestructure.Context;
using Cuca_Api.Infraestructure.Exceptions;
using PD_Api.Infraestructure.Repository;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Services.Usuario;

namespace PD_Api.Services
{
    public class ProdutoService
    {
        private readonly MainDBContext _dbContext;
        private readonly ProdutoRep _produtoRep;
        public ProdutoService(MainDBContext dbContext, ProdutoRep produtoRep)
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
