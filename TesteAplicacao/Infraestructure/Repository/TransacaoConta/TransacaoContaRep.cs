using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class TransacaoContaRep : RepositoryBase<TransacaoConta>, ITransacoesContaRep
    {

        public TransacaoContaRep(MainDBContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
