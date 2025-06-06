using System;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Cuca_Api.Infraestrutra.Extensions;

namespace Cuca_Api.Infraestrutra.Responses
{
    public class HttpErrorResponse
    {
        public int statusCode { get; private set; }
        public string statusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string mensagem { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object detalhes { get; private set; }

        public List<ValidationError> errors { get; }

        public HttpErrorResponse(HttpStatusCode statusCode)
        {
            this.statusCode = (int)statusCode;
            statusDescription = statusCode.ToString();
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string message) : this(statusCode)
        {
            mensagem = message;
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string message, object details) : this(statusCode, message)
        {
            detalhes = details;
        }

        public HttpErrorResponse(HttpStatusCode statusCode, string message, ModelStateDictionary modelState) : this(statusCode)
        {
            mensagem = message;
            errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, 0, x.ErrorMessage)))
                    .ToList();
        }

        
    }
}

