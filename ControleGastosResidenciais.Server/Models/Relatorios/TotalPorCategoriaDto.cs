namespace ControleGastosResidenciais.Server.Models.Relatorios;

// Classe total por categoria com suas propriedades
public class TotalPorCategoriaDto
{
    public long IdCategoria { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public double TotalReceitas { get; set; }
    public double TotalDespesas { get; set; }

    public double Saldo => TotalReceitas - TotalDespesas;
}
