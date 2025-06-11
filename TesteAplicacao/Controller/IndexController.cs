using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.Infraestructure.ParameterStore;
using TesteAplicacao.Infraestrutra.Responses;

namespace TesteAplicacao.Controller
{
    [ApiController]
    public class IndexController : BaseController
    {
        private readonly IConfiguration configuration;

        public IndexController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("ambiente")]
        [AllowAnonymous]
        public async Task<IActionResult> Ambiente()
        {
            return Ok(new HttpOkResponse<String>("Ambiente:", ParameterStore.Ambiente(configuration)));
        }
    }
}
