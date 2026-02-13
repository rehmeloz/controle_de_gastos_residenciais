using Dapper;
using ControleGastosResidenciais.Server.Models;

namespace ControleGastosResidenciais.Server.Data.Repositories;

/* Repositório de categorias, onde herda os métodos de ICategoriaRepository (Interface de categorias), e retorna a listagem de todas as categorias
e faz a inserção de uma nova */
public class CategoriaRepository : ICategoriaRepository
{
    public IEnumerable<Categoria> ObterTodas()
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            SELECT Id, Descricao, Finalidade
            FROM Categorias
        ";

        return connection.Query<Categoria>(sql);
    }

    public int Inserir(Categoria categoria)
    {
        using var connection = DatabaseConfig.GetConnection();

        const string sql = @"
            INSERT INTO Categorias (Descricao, Finalidade)
            VALUES (@Descricao, @Finalidade);
            SELECT last_insert_rowid();
        ";

        return connection.ExecuteScalar<int>(sql, categoria);
    }
}
