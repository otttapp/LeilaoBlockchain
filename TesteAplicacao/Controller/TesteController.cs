using Cuca_Api.DTO;
using Cuca_Api.Infraestrutra.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Cuca_Api.Controller
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/teste")]
    [ApiController]
    public class TesteController : BaseController
    {
        //private readonly RotacoesService _rotacoesSerivce;


        //public TesteController(RotacoesService rotacoesService)
        //{
        //    _rotacoesSerivce = rotacoesService;
        //}

    }
}
