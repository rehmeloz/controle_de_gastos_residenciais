using ControleGastosResidenciais.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Server.Data.Mappings;

public class TransacoesMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(d => d.Descricao).IsRequired().HasColumnType("TEXT").HasMaxLength(400);
        builder.Property(v => v.Valor).IsRequired().HasColumnType("REAL");
        builder.Property(t => t.Tipo).IsRequired().HasColumnType("INTEGER");
        builder.Property(x => x.IdPessoa).IsRequired().HasColumnType("INTEGER");
        builder.Property(x => x.IdCategoria).IsRequired().HasColumnType("INTEGER");
        builder.HasOne(t => t.Pessoa).WithMany(t => t.Transacao).HasForeignKey(t => t.IdPessoa).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Categoria).WithMany(t => t.Transacao).HasForeignKey(t => t.IdCategoria);
    }
}
