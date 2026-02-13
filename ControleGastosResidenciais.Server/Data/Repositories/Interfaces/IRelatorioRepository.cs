using ControleGastosResidenciais.Server.Models.Relatorios;

// Interface para relatórios (consultas), possuido um método para obter totais por pessoa e outro para obter totais por categoria
public interface IRelatorioRepository
{
    TotaisPorPessoaResponse ObterTotaisPorPessoa();
    TotaisPorCategoriaResponse ObterTotaisPorCategoria();
}
