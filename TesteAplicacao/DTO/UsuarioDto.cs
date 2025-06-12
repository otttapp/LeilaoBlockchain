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
}
