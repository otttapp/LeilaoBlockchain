namespace Cuca_Api.Infraestructure.ParameterStore
{
    /**
     * 
     * Classe responsável por gerenciar as variáveis do parameter store
     * 
     */
    public class ParameterStore
    {
        public ParameterStore()
        {
        }

        public static string Ambiente(IConfiguration config)
        {
            string valor = string.Empty;
            try
            {
                var env = config.GetSection("iis:env").GetChildren();
                if (env != null)
                {
                    foreach (var envKeyValue in env)
                    {
                        var splitKeyValue = envKeyValue.Value.Split('=');
                        var envKey = splitKeyValue[0];
                        var envValue = splitKeyValue[1];
                        if (envKey == "ASPNETCORE_ENVIRONMENT")
                        {
                            valor = envValue.ToLower();
                        }
                    }
                }
                var aspnetenv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                
                if (aspnetenv != null)
                    valor = aspnetenv.ToLower();
            }
            catch { }

            return valor;
        }
    }
}
