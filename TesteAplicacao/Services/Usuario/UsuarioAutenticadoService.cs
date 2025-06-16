using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Interfaces;

public class UsuarioAutenticadoService
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUsuarioRep _usuarioRep;

    public UsuarioAutenticadoService(
        IHttpContextAccessor httpContext,
        IUsuarioRep usuarioRep)
    {
        _httpContext = httpContext;
        _usuarioRep = usuarioRep;
    }
    private byte[] GetHashFromHeader()
    {
        var header = _httpContext.HttpContext?
            .Request
            .Headers["Authorization"]
            .FirstOrDefault()
            ?? string.Empty;

        if (!header.StartsWith("Bearer "))
            throw new BusinessException("TOKEN_INVALIDO", "Header Authorization mal formatado.");

        var base64 = header.Substring("Bearer ".Length);
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch
        {
            throw new BusinessException("TOKEN_INVALIDO", "Falha ao decodificar token.");
        }
    }

    public async Task<Usuario> GetUsuarioLogado()
    {
        var hashBytes = GetHashFromHeader();

        var usuario = await _usuarioRep.GetByHashAsync(hashBytes);
        if (usuario is null)
            throw new BusinessException("USUARIO_NAO_ENCONTRADO", "Token inválido ou usuário não existe.");

        if (!usuario.ativo)
            throw new BusinessException("USUARIO_INATIVO", "Usuário está inativo.");

        return usuario;
    }

    public async Task<uint> GetUsuarioLogadoId()
    {
        return (await GetUsuarioLogado()).usuario_id;
    }
}
