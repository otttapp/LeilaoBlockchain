using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class UsuarioRep : RepositoryBase<Usuario>, IUsuarioRep
    {
        //private readonly IMapper _mapper;

        public UsuarioRep(MainDBContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
