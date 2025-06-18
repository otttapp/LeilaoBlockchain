using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TesteAplicacao.Entities
{
    public class Usuario
    {
        public uint usuario_id { get; set; }
        public string nome { get; set; } = null!;
        public byte[] senha_hash { get; set; } = null!;
        public byte[] senha_salt { get; set; } = null!;
        public string? email { get; set; }
        public string? telefone { get; set; }
        public bool ativo { get; set; }
        public DateTime? datahora_insercao { get; set; }
        public DateTime? datahora_desativacao { get; set; }

        public ICollection<Produto> produtos { get; set; } = new List<Produto>();
        public virtual Conta Conta { get; set; } = null!;

    }

}