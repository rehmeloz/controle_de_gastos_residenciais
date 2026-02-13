namespace ControleGastosResidenciais.Server.Models.Relatorios;

// Classe de totais por categoria, possuindo duas propriedades, uma que lista o total por categoria, e outra o total geral
public class TotaisPorCategoriaResponse
{
    public List<TotalPorCategoriaDto> Categorias { get; set; } = new();
    public TotalGeralDto TotalGeral { get; set; } = new();
}
