using System.Net;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Cuca_Api.Infraestructure.Exceptions;
using Cuca_Api.Infraestrutra.Responses;

namespace Cuca_Api.Infraestructure.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {

        private readonly ILogger<GlobalExceptionHandlerMiddleware> log;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> log, IServiceScopeFactory serviceScopeFactory)
        {
            this.log = log;
            this._serviceScopeFactory = serviceScopeFactory;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var endpoint = context.GetEndpoint();
                await next(context);
            }
            catch (Exception ex)
            {
                log.LogError(ex, JsonConvert.SerializeObject(ex.InnerException));
                log.LogError(ex, JsonConvert.SerializeObject(ex.Message));
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            var settingsJson = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (exception is BusinessException businessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                if (!(businessException.Details is null))
                {
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(
                        new HttpErrorResponse(
                            HttpStatusCode.UnprocessableEntity,
                            businessException.Message,
                            new
                            {
                                ErrorType = "BusinessException",
                                ErrorCode = businessException.Identifier,
                                ErrorDetails = businessException.Details
                            }
                        )
                    , settingsJson));
                }

                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new HttpErrorResponse(
                        HttpStatusCode.UnprocessableEntity,
                        businessException.Message,
                        new
                        {
                            ErrorType = "BusinessException",
                            ErrorIdentifier = businessException.Identifier
                        }
                    )
                , settingsJson));
            }
            else
            {
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new HttpErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while processing the request", exception), settingsJson));
        }
    }
}
