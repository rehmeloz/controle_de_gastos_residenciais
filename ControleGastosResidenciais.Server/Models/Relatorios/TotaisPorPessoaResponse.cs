namespace ControleGastosResidenciais.Server.Models.Relatorios;

// Classe de totais por pessoa, possuindo duas propriedades uma que lista o total por pessoa, e outra o total geral
public class TotaisPorPessoaResponse
{
    public List<TotalPorPessoaDto> Pessoas { get; set; } = new();
    public TotalGeralDto TotalGeral { get; set; } = new();
}
