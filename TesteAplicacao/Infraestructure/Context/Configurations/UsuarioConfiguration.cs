using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAplicacao.Entities;

namespace TesteAplicacao.Infraestructure.Context.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public virtual void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(u => u.usuario_id);

            builder.Property(u => u.usuario_id)
                .HasColumnName("usuario_id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.senha_hash)
                .HasColumnName("senha_hash")
                .IsRequired()
                .HasColumnType("varbinary(64)");

            builder.Property(u => u.senha_salt)
                .HasColumnName("senha_salt")
                .IsRequired()
                .HasColumnType("varbinary(64)");

            builder.Property(u => u.email)
                .HasColumnName("email")
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(u => u.telefone)
                .HasColumnName("telefone")
                .IsRequired(false)
                .HasMaxLength(15);

            builder.Property(u => u.ativo)
                .HasColumnName("ativo")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.datahora_insercao)
                .HasColumnName("datahora_insercao")
                .IsRequired(false)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.datahora_desativacao)
                .HasColumnName("datahora_desativacao")
                .IsRequired(false);

            // Adicionando relação 1:N com Produto
            builder
                .HasMany(u => u.produtos)
                .WithOne(p => p.usuario)
                .HasForeignKey(p => p.usuario_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
