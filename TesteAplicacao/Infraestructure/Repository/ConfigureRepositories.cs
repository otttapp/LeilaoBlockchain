using PD_Api.Infraestructure.Interfaces;
using PD_Api.Infraestructure.Repository;

namespace Cuca_Api.Infraestructure.Repository
{
    public static class ConfigureRepositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRep, ProdutoRep>();
        }
    }
}
