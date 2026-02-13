namespace ControleGastosResidenciais.Server.Models.Relatorios;

// Classe total por pessoa com suas propriedades
public class TotalPorPessoaDto
{
    public long IdPessoa { get; set; }
    public string Nome { get; set; } = string.Empty;

    public double TotalReceitas { get; set; }
    public double TotalDespesas { get; set; }

    public double Saldo => TotalReceitas - TotalDespesas;
}
