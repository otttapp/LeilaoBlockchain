using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TesteAplicacao.Infraestructure.Exceptions; 

namespace TesteAplicacao.Infraestructure.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro.");

                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    BusinessException => (int)HttpStatusCode.UnprocessableEntity, 
                    _ => (int)HttpStatusCode.InternalServerError                
                };

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    message = ex.Message,
                    exceptionType = ex.GetType().Name
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };

                var json = JsonConvert.SerializeObject(response, jsonSettings);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
