using TesteAplicacao.Services.Usuario;

namespace PD_Api.Services
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
