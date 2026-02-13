using Dapper;
using ControleGastosResidenciais.Server.Models;

namespace ControleGastosResidenciais.Server.Data.Repositories;

/* Repositório de pessoas, onde herda os métodos de IPessoasRepository (Interface de pessoas), retornando todas as pessoas criadas,
faz a inserção de uma nova pessoa e excluí uma pessoa (Ao excluir uma pessoa as transações realizadas por ela também são excluídas)*/
public class PessoaRepository : IPessoasRepository
{
    public IEnumerable<Pessoa> ObterTodas()
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            SELECT Id, Nome, Idade
            FROM Pessoas
        ";

        return connection.Query<Pessoa>(sql);
    }

    public int Inserir(Pessoa pessoa)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            INSERT INTO Pessoas (Nome, Idade)
            VALUES (@Nome, @Idade);
            SELECT last_insert_rowid();
        ";

        return connection.ExecuteScalar<int>(sql, pessoa);
    }

    public void Remover(int id)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string deleteTransacoes = "DELETE FROM Transacoes WHERE IdPessoa = @Id";
        connection.Execute(deleteTransacoes, new { Id = id });

        const string deletePessoa = "DELETE FROM Pessoas WHERE Id = @Id";
        connection.Execute(deletePessoa, new { Id = id });
    }

    public async Task<Pessoa?> ObterPorIdAsync(int id)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            SELECT Id, Nome, Idade
            FROM Pessoas
            WHERE Id = @Id
        ";

        return await connection.QueryFirstOrDefaultAsync<Pessoa>(sql, new { Id = id });
    }

    public async Task AtualizarAsync(Pessoa pessoa)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            UPDATE Pessoas
            SET Nome = @Nome,
                Idade = @Idade
            WHERE Id = @Id
        ";

        await connection.ExecuteAsync(sql, pessoa);
    }
}
