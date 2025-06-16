namespace TesteAplicacao.Entities
{
    public class Produto
    {
        public uint produto_id { get; set; }
        
        public string nome { get; set; } = null!;
        
        public bool ativo { get; set; }
        
        public string? descricao { get; set; } = null!;
        
        public DateTime? data_compra { get; set; }
        
        public DateTime? datahora_insercao { get; set; }    
        
        public decimal valor { get; set; }
        
        public uint raridade{ get; set; }


        //public uint usuario_id { get; set; }

        //public Usuario usuario { get; set; } = null!;

    }
}
