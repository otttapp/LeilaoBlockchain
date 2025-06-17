namespace TesteAplicacao.DTO
{
    public class InserirUsuarioRequestDto
    {
        public string nome { get; set; } = null!;
        public string senha { get; set; } = null!;
        public string? email { get; set; }
        public string? telefone { get; set; }
    }   

    public class AlterarUsuarioRequestDto
    {
        public string nome { get; set; } = null!;
        public string senha { get; set; } = null!;
        public string? email { get; set; }
        public string? telefone { get; set; }
    }

    public class AlterarSenhaUsuarioRequestDto
    {
        public string SenhaAtual { get; set; } = null!;
        public string NovaSenha { get; set; } = null!;
    }

    public class LoginRequestDto
    {
        public required string email { get; set; }
        public required string password { get; set; }
        public string newPassword { get; set; } = null!;
    }
    public class UsuarioResponseDto
    {
        public string email { get; set; } = null!;
        public string? token { get; set; }
        public string? password { get; set; } 
        public uint? ultima_empresa_logada_id { get; set; }
    }

    public class GetUsuariosDto
    {
        public uint usuario_id { get; set; }
        public string nome { get; set; } = string.Empty;
        public bool ativo { get; set; }
        public string email { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public DateTime? datahora_insercao { get; set; }
        public DateTime? datahora_desativacao { get; set; }
    }
}
