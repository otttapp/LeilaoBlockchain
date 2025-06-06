using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Cuca_Api.Infraestrutra.Responses;

namespace Cuca_Api.Infraestrutra.Extensions
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string field { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int code { get; set; }

        public string mensagem { get; }

        public ValidationError(string field, int code, string message)
        {
            this.field = field != string.Empty ? field : null;
            this.code = code != 0 ? code : 55;  //set the default code to 55. you can remove it or change it to 400.  
            mensagem = message;
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new HttpErrorResponse(System.Net.HttpStatusCode.UnprocessableEntity, "Falha ao validar requisição", modelState))
        {
            //StatusCode = StatusCodes.Status422UnprocessableEntity; //change the http status code to 422.  
        }
    }

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }


}

