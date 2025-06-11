using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TesteAplicacao.Infraestrutra.Extensions;

namespace TesteAplicacao.Infraestrutra.Responses
{
    public class HttpErrorResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; private set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; private set; }

        [JsonProperty("mensagem", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Mensagem { get; private set; }

        [JsonProperty("detalhes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Detalhes { get; private set; }

        [JsonProperty("errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ValidationError> Errors { get; private set; }

        public HttpErrorResponse(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
            StatusDescription = statusCode.ToString();
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string mensagem)
            : this(statusCode)
        {
            Mensagem = mensagem;
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string mensagem, object detalhes)
            : this(statusCode, mensagem)
        {
            Detalhes = detalhes;
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string mensagem, ModelStateDictionary modelState)
            : this(statusCode, mensagem)
        {
            if (modelState != null)
            {
                Errors = modelState
                    .Where(ms => ms.Value.Errors.Any())
                    .SelectMany(ms => ms.Value.Errors.Select(error => new ValidationError(ms.Key, 0, error.ErrorMessage)))
                    .ToList();
            }
        }
    }
}
