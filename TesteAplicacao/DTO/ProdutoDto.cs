using TesteAplicacao.Entities;

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
    
    public class ProdutoResponseDto
    {
        public uint produto_id { get; set; }

        public string nome { get; set; } = null!;

        public bool ativo { get; set; }

        public string descricao { get; set; } = null!;

        public DateOnly data_compra { get; set; }

        public DateTime? datahora_insercao { get; set; }

        public decimal valor { get; set; }

        public uint raridade { get; set; }

        public Usuario usuario { get; set; } = null!;
    }
}
