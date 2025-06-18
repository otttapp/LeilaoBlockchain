namespace TesteAplicacao.DTO
{
    public class InserirProdutoRequestDto
    {
        public string nome { get; set; } = null!;

        public bool ativo { get; set; }

        public string descricao { get; set; } = null!;

        public decimal valor { get; set; }

        public uint raridade { get; set; }
    }

    public class AlterarProdutoRequestDto
    {
        public string nome { get; set; } = null!;

        public string descricao { get; set; } = null!;

        public decimal valor { get; set; }

        public uint raridade { get; set; }
    }
    public class GetPodutosDto
    {
        public uint produto_id { get; set; }

        public string nome { get; set; } = null!;

        public bool ativo { get; set; }

        public string descricao { get; set; } = null!;

        public DateTime? data_compra { get; set; }

        public DateTime? datahora_insercao { get; set; }

        public decimal valor { get; set; }

        public uint raridade { get; set; }

        public UsuarioBaseDto usuario { get; set; } = null!;
    }

    public class UsuarioBaseDto
    {
        public uint usuario_id { get; set; }
        public string nome { get; set; } = string.Empty;
    }
}
