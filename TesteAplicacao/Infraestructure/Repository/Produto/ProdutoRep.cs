using Azure;
using Cuca_Api.Infraestructure.Context;
using Cuca_Api.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using PD_Api.Infraestructure.Interfaces;
using TesteAplicacao.Entities;

namespace PD_Api.Infraestructure.Repository
{
    public class ProdutoRep : RepositoryBase<Produto>, IProdutoRep
    {
        //private readonly IMapper _mapper;

        public ProdutoRep(MainDBContext repositoryContext) : base(repositoryContext)
        {
            
        }


    }
}

