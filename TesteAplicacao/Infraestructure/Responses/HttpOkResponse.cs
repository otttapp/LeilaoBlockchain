using Newtonsoft.Json;

namespace Cuca_Api.Infraestrutra.Responses
{
    public class HttpOkResponse<T>
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string mensagem { get; private set; }

        public T data { get; set; }

        public HttpOkResponse()
        {
            
        }

        public HttpOkResponse(string Mensagem)
        {
            this.mensagem = Mensagem;
        }

        public HttpOkResponse(string Mensagem, T Data)
        {
            this.mensagem = Mensagem;
            this.data = Data;
        }

        public HttpOkResponse(T Data)
        {
            this.data = Data;
        }
    }
}