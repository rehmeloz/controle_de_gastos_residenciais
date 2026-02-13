using Dapper;
using ControleGastosResidenciais.Server.Models;

namespace ControleGastosResidenciais.Server.Data.Repositories;

/* Repositório de transações, onde herda os métodos de ITransacoesRepository (Interface de transações), retornando todas as transações 
 realizadas e podendo fazer a inserção de uma nova */
public class TransacaoRepository : ITransacoesRepository
{
    public IEnumerable<Transacao> ObterTodas()
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            SELECT * FROM Transacoes
        ";

        return connection.Query<Transacao>(sql);
    }

    public int Inserir(Transacao transacao)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            INSERT INTO Transacoes (Descricao, Valor, Tipo, IdPessoa, IdCategoria)
            VALUES (@Descricao, @Valor, @Tipo, @IdPessoa, @IdCategoria);
            SELECT last_insert_rowid();
        ";

        return connection.ExecuteScalar<int>(sql, transacao);
    }
}
