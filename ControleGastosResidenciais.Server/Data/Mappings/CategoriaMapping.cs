using ControleGastosResidenciais.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ControleGastosResidenciais.Server.Data.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(d => d.Descricao).IsRequired().HasColumnType("TEXT").HasMaxLength(400);
        builder.Property(f => f.Finalidade).IsRequired().HasColumnType("INTEGER");
    }
}
