namespace ControleGastosResidenciais.Server.Data;
using Microsoft.Data.Sqlite;

// Configuração de banco de dados com Sqlite
public static class DatabaseConfig
{
    private const string ConnectionString = "Data Source=controle_gastos.db";

    public static SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}

