namespace TesteAplicacao.Entities
{
    public class TransacaoConta
    {
        public uint transacao_conta_id { get; set; }
        public decimal valor { get; set; }
        public DateTime datahora_transacao { get; set; }
        public string? descricao { get; set; }

        public uint conta_id { get; set; }
        public virtual Conta conta { get; set; } = null!;
    }

}
