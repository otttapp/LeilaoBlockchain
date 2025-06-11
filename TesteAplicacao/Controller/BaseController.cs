using Microsoft.AspNetCore.Mvc;
using TesteAplicacao.Infraestrutra.Responses;
using System.Net;

namespace TesteAplicacao.Controller
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        public BaseController()
        {

        }

        [NonAction]
        public IActionResult Ok(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.OK, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.InternalServerError" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.InternalServerError" /></returns>
        [NonAction]
        public IActionResult InternalServerError()
        {
            return InternalServerError(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.InternalServerError" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.InternalServerError" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult InternalServerError(string message)
        {
            return InternalServerError(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.InternalServerError" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.InternalServerError" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult InternalServerError(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.InternalServerError, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.NotFound" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.NotFound" /></returns>
        [NonAction]
        public IActionResult NotFound()
        {
            return NotFound(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.NotFound" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.NotFound" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult NotFound(string message)
        {
            return NotFound(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.NotFound" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.NotFound" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult NotFound(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.NotFound, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Forbidden" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Forbidden" /></returns>
        [NonAction]
        public IActionResult Forbidden()
        {
            return Forbidden(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Forbidden" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Forbidden" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult Forbidden(string message)
        {
            return Forbidden(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Forbidden" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Forbidden" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult Forbidden(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.Forbidden, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Unauthorized" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Unauthorized" /></returns>
        [NonAction]
        public IActionResult Unauthorized()
        {
            return Unauthorized(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Unauthorized" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Unauthorized" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult Unauthorized(string message)
        {
            return Unauthorized(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.Unauthorized" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.Unauthorized" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult Unauthorized(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.Unauthorized, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.BadRequest" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.BadRequest" /></returns>
        [NonAction]
        public IActionResult BadRequest()
        {
            return BadRequest(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.BadRequest" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.BadRequest" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult BadRequest(string message)
        {
            return BadRequest(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.BadRequest" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.BadRequest" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult BadRequest(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.BadRequest, message, details);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.UnprocessableEntity" />
        /// </summary>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.UnprocessableEntity" /></returns>
        [NonAction]
        public IActionResult UnprocessableEntity()
        {
            return UnprocessableEntity(null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.UnprocessableEntity" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.UnprocessableEntity" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult UnprocessableEntity(string message)
        {
            return UnprocessableEntity(message, null);
        }

        /// <summary>
        /// Retorna um <see cref="ObjectResult" /> com um código de status <see cref="HttpStatusCode.UnprocessableEntity" />
        /// e uma mensagem especificada por <paramref name="message"/>
        /// </summary>
        /// <param name="message">Mensagem a ser anexada ao <see cref="ObjectResult" /></param>
        /// <param name="details">Detalhes a serem anexados ao <see cref="ObjectResult" /></param>
        /// <returns>Resposta HTTP com status <see cref="HttpStatusCode.UnprocessableEntity" /> e mensagem <paramref name="message"/></returns>
        [NonAction]
        public IActionResult UnprocessableEntity(string message, object details)
        {
            return CreateStatusCodeResponse(HttpStatusCode.UnprocessableEntity, message, details);
        }

        /// <summary>
        /// Retorna uma instância de <see cref="HttpErrorResponse" /> como uma resposta HTTP
        /// </summary>
        /// <param name="httpStatusCode">Status code da resposta HTTP</param>
        /// <param name="message">Mensagem a ser incluída no corpo da resposta</param>
        /// <param name="details">Detalhes a serem incluídos no corpo da resposta</param>
        /// <returns></returns>
        [NonAction]
        private IActionResult CreateStatusCodeResponse(HttpStatusCode httpStatusCode, string message, object details)
        {
            HttpErrorResponse httpErrorResponse = new HttpErrorResponse(httpStatusCode, message, details);
            return StatusCode(httpErrorResponse.StatusCode, httpErrorResponse);
        }
    }
}
