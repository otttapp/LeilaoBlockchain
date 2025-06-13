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

}
