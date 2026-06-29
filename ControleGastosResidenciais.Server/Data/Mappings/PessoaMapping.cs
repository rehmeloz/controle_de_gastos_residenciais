using ControleGastosResidenciais.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ControleGastosResidenciais.Server.Data.Mappings;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(n => n.Nome).IsRequired().HasColumnType("TEXT").HasMaxLength(200);
        builder.Property(d => d.Idade).IsRequired().HasColumnType("INTEGER");
    }
}
