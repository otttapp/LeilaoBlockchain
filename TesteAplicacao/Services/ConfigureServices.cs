using TesteAplicacao.Infraestructure.Repository;

namespace TesteAplicacao.Services
{
    public static class ConfigureServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMvc(); 
            services.AddScoped<ProdutoService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<UsuarioAutenticadoService>();
            services.AddScoped<ContaService>();

        }
    }
}
