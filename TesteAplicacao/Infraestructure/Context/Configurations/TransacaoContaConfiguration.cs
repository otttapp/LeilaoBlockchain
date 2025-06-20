using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Context.Configurations
{
    public class TransacaoContaConfiguration : IEntityTypeConfiguration<TransacaoConta>
    {
        public virtual void Configure(EntityTypeBuilder<TransacaoConta> builder)
        {
            builder.ToTable("transacao_conta");

            builder.HasKey(t => t.transacao_conta_id);

            builder.Property(t => t.transacao_conta_id)
                .HasColumnName("transacao_conta_id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.valor)
                .HasColumnName("valor")
                .IsRequired();

            builder.Property(t => t.datahora_transacao)
                .HasColumnName("datahora")
                .IsRequired();

            builder.Property(t => t.descricao)
                .HasColumnName("descricao")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(t => t.conta_id)
                .HasColumnName("conta_id")
                .IsRequired();

            builder.HasOne(t => t.conta)
                .WithMany() // Se você tiver lista de transações na entidade Conta, troque por: .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.conta_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
