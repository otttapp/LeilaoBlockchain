using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cuca_Api.Infraestructure.ParameterStore;
using Cuca_Api.Infraestrutra.Responses;

namespace Cuca_Api.Controller
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
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
