using Cuca_Api.Infraestructure.Interfaces.membros;
using Cuca_Api.Infraestructure.Interfaces.Rotacoes;
using Cuca_Api.Infraestructure.Repository;

namespace Cuca_Api.Services
{
    public static class ConfigureServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<MembrosServices>();
            services.AddScoped<RotacoesService>();
        }
    }
}
