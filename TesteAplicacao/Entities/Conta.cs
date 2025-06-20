namespace TesteAplicacao.Entities
{
    public class Conta
    {
        public uint conta_id { get; set; }
        public uint usuario_id { get; set; }
        public string numero { get; set; } = string.Empty;
        public string banco { get; set; } = string.Empty;
        public bool ativa { get; set; } = true;
        public DateTime? data_criacao { get; set; }

        public decimal? saldo_total { get; set; }
        public decimal? saldo_disponivel { get; set; }
        public decimal? saldo_pendente { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;

        public virtual ICollection<TransacaoConta> Transacoes { get; set; } = new List<TransacaoConta>();

    }

}
