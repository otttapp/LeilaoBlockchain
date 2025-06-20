using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Context.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("conta");

            builder.HasKey(c => c.conta_id);

            builder.Property(c => c.conta_id)
                .HasColumnName("conta_id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.usuario_id)
                .HasColumnName("usuario_id")
                .IsRequired();

            builder.Property(c => c.numero)
                .HasColumnName("numero")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.banco)
                .HasColumnName("banco")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ativa)
                .HasColumnName("ativa")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(c => c.data_criacao)
                .HasColumnName("data_criacao")
                .IsRequired(false);

            builder.Property(c => c.saldo_total)
                .HasColumnName("saldo_total")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.saldo_disponivel)
                .HasColumnName("saldo_disponivel")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.saldo_pendente)
                .HasColumnName("saldo_pendente")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.Usuario)
                .WithOne(u => u.Conta)
                .HasForeignKey<Conta>(c => c.usuario_id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Transacoes)
            .WithOne(t => t.conta)
            .HasForeignKey(t => t.conta_id)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
