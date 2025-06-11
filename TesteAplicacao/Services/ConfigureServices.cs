using TesteAplicacao.Infraestructure.Repository;
using TesteAplicacao.Services.Usuario;

namespace TesteAplicacao.Services
{
    public static class ConfigureServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMvc(); 
            services.AddScoped<ProdutoService>();
        }
    }
}
