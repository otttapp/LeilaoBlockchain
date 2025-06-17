using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Middleware
{
    public class HashAuthenticationMiddleware : IMiddleware
    {
        private readonly IUsuarioRep _usuarioRep;

        public HashAuthenticationMiddleware(IUsuarioRep usuarioRep)
        {
            _usuarioRep = usuarioRep;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
            {
                await next(context);
                return;
            }

            var header = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(header) || !header.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token não informado.");
                return;
            }

            byte[] hashBytes;
            try
            {
                var base64 = header.Substring("Bearer ".Length);
                hashBytes = Convert.FromBase64String(base64);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token mal formatado.");
                return;
            }

            var usuario = await _usuarioRep.GetByHashAsync(hashBytes);
            if (usuario == null || !usuario.ativo)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido ou usuário inativo.");
                return;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.usuario_id.ToString()),
                new Claim(ClaimTypes.Name,           usuario.nome),
                new Claim(ClaimTypes.Email,          usuario.email ?? string.Empty)
            };
            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "HashAuth"));

            await next(context);
        }
    }
}
