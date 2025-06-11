using Azure;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using TesteAplicacao.Infraestructure.Interfaces;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class ProdutoRep : RepositoryBase<Produto>, IProdutoRep
    {
        //private readonly IMapper _mapper;

        public ProdutoRep(MainDBContext repositoryContext) : base(repositoryContext)
        {
            
        }


    }
}

