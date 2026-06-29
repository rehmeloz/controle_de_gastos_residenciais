namespace ControleGastosResidenciais.Server.Data;
using ControleGastosResidenciais.Server.Data.Mappings;
using ControleGastosResidenciais.Server.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaMapping());
        modelBuilder.ApplyConfiguration(new PessoaMapping());
        modelBuilder.ApplyConfiguration(new TransacoesMapping());
    }
}
