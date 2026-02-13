using Dapper;
using ControleGastosResidenciais.Server.Data;
using ControleGastosResidenciais.Server.Models.Relatorios;
using ControleGastosResidenciais.Server.Enums;

/* Repositório de relatórios, que herda os métodos de IRelatorioRepository (Interface de relatórios), retornando os totais por pessoa,
 e os totais por categoria, além de o total geral */
public class RelatorioRepository : IRelatorioRepository
{
    public TotaisPorPessoaResponse ObterTotaisPorPessoa()
    {
        using var connection = DatabaseConfig.GetConnection();

        var selectPessoa = @"
            SELECT
                p.Id AS IdPessoa,
                p.Nome AS Nome,
                COALESCE(
                    SUM(CASE WHEN t.Tipo = @Receita THEN t.Valor END),
                    0.0
                ) AS TotalReceitas,
                COALESCE(
                    SUM(CASE WHEN t.Tipo = @Despesa THEN t.Valor END),
                    0.0
                ) AS TotalDespesas
            FROM Pessoas p
            LEFT JOIN Transacoes t ON t.IdPessoa = p.Id
            GROUP BY p.Id, p.Nome
            ORDER BY p.Nome;
        ";

        var totaisPorPessoa = connection
            .Query<TotalPorPessoaDto>(
                selectPessoa,
                new
                {
                    Receita = (int)TipoTransacao.Receita,
                    Despesa = (int)TipoTransacao.Despesa
                }
            )
            .AsList();

        var selectGeral = @"
            SELECT
                COALESCE(SUM(CASE WHEN Tipo = @Receita THEN Valor END), 0) AS TotalReceitas,
                COALESCE(SUM(CASE WHEN Tipo = @Despesa THEN Valor END), 0) AS TotalDespesas
            FROM Transacoes;
        ";

        var totalGeral = connection.QuerySingle<TotalGeralDto>(
            selectGeral,
            new
            {
                Receita = (int)TipoTransacao.Receita,
                Despesa = (int)TipoTransacao.Despesa
            }
        );

        return new TotaisPorPessoaResponse
        {
            Pessoas = totaisPorPessoa,
            TotalGeral = totalGeral
        };
    }

    public TotaisPorCategoriaResponse ObterTotaisPorCategoria()
    {
        using var connection = DatabaseConfig.GetConnection();

        var selectCategorias = @"
            SELECT
                c.Id AS IdCategoria,
                c.Descricao,
                COALESCE(
                    SUM(CASE WHEN t.Tipo = @Receita THEN t.Valor END),
                    0.0
                ) AS TotalReceitas,
                COALESCE(
                    SUM(CASE WHEN t.Tipo = @Despesa THEN t.Valor END),
                    0.0
                ) AS TotalDespesas
            FROM Categorias c
            LEFT JOIN Transacoes t ON t.IdCategoria = c.Id
            GROUP BY c.Id, c.Descricao
            ORDER BY c.Descricao;
        ";

        var categorias = connection.Query<TotalPorCategoriaDto>(
            selectCategorias,
            new
            {
                Receita = (int)TipoTransacao.Receita,
                Despesa = (int)TipoTransacao.Despesa
            }
        ).AsList();

        var selectGeral = @"
            SELECT
                COALESCE(SUM(CASE WHEN Tipo = @Receita THEN Valor END), 0) AS TotalReceitas,
                COALESCE(SUM(CASE WHEN Tipo = @Despesa THEN Valor END), 0) AS TotalDespesas
            FROM Transacoes;
        ";

        var totalGeral = connection.QuerySingle<TotalGeralDto>(
            selectGeral,
            new
            {
                Receita = (int)TipoTransacao.Receita,
                Despesa = (int)TipoTransacao.Despesa
            }
        );

        return new TotaisPorCategoriaResponse
        {
            Categorias = categorias,
            TotalGeral = totalGeral
        };
    }
}
