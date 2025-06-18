using TesteAplicacao.Entities;

namespace TesteAplicacao.DTO
{
    public class InserirContaRequestDto
    {
        public string banco { get; set; } = string.Empty;
        public bool ativa { get; set; } = true;
        public decimal saldo_disponivel { get; set; }
        public decimal saldo_pendente { get; set; }
    }

    public class GetContasDto
    {
        public uint conta_id { get; set; }
        public uint usuario_id { get; set; }
        public string numero { get; set; } = string.Empty;
        public string banco { get; set; } = string.Empty;
        public bool ativa { get; set; } = true;
        public DateTime? data_criacao { get; set; }
        public decimal saldo_total { get; set; }
        public decimal saldo_disponivel { get; set; }
        public decimal saldo_pendente { get; set; }
        public virtual UsuarioBaseDto Usuario { get; set; } = null!;
    }
}
