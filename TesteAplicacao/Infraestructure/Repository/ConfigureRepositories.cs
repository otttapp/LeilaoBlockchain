using TesteAplicacao.Infraestructure.Interfaces;
using TesteAplicacao.Infraestructure.Repository;

namespace TesteAplicacao.Infraestructure.Repository
{
    public static class ConfigureRepositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRep, ProdutoRep>();
            services.AddScoped<IUsuarioRep, UsuarioRep>();
            services.AddScoped<IContaRep, ContaRep>();
            services.AddScoped<ITransacoesContaRep, TransacaoContaRep>();
        }
    }
}
