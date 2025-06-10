using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TesteAplicacao.Entities;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produto");

        builder.HasKey(p => p.produto_id);

        builder.Property(p => p.produto_id)
            .HasColumnName("produto_id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(p => p.descricao)
            .HasColumnName("descricao")
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(p => p.data_compra)
            .HasColumnName("data_compra")
            .IsRequired(false);

        builder.Property(p => p.datahora_insercao)
            .HasColumnName("datahora_insercao")
            .IsRequired(false);

        builder.Property(p => p.valor)
            .HasColumnName("valor")
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.raridade)
            .HasColumnName("raridade")
            .IsRequired();

        builder.Property(p => p.usuario_id)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.HasOne(p => p.usuario)
            .WithMany()
            .HasForeignKey(p => p.usuario_id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
