namespace ControleGastosResidenciais.Server.Models.Relatorios;

// Classe de total geral, que é mostrado na página de relatórios, exibindo o total de receitas, despesas e o saldo
public class TotalGeralDto
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    public decimal Saldo => TotalReceitas - TotalDespesas;
}
