using ControleGastosResidenciais.Server.Models.Relatorios;

// Interface de serviço de relatórios, retornando a obtenção de totais por pessoa e o total por categoria
public interface IRelatorioService
{
    TotaisPorPessoaResponse ObterTotaisPorPessoa();
    TotaisPorCategoriaResponse ObterTotaisPorCategoria();
}
