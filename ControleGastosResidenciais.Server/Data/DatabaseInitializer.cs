namespace ControleGastosResidenciais.Server.Data;
using Dapper;

// Inicializador de banco de dados, sendo a primeira vez gerando o projeto as tabelas são criadas e alimentadas com dados iniciais
public static class DatabaseInitializer
{
    public static void Initialize()
    {
        using var connection = DatabaseConfig.GetConnection();

        connection.Execute("PRAGMA foreign_keys = ON;");

        // Criação das tabelas
        connection.Execute(@"
        CREATE TABLE IF NOT EXISTS Pessoas (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL CHECK (length(Nome) <= 200),
            Idade INT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Categorias (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Descricao TEXT NOT NULL CHECK (length(Descricao) <= 400),
            Finalidade INTEGER NOT NULL CHECK (Finalidade IN (1, 2, 3))
        );

        CREATE TABLE IF NOT EXISTS Transacoes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Descricao TEXT NOT NULL CHECK (length(Descricao) <= 400),
            Valor REAL NOT NULL CHECK (Valor > 0),
            Tipo INTEGER NOT NULL CHECK (Tipo IN (1, 2)),
            IdPessoa INTEGER NOT NULL,
            IdCategoria INTEGER NOT NULL,
            FOREIGN KEY (IdPessoa) REFERENCES Pessoas(Id) ON DELETE CASCADE,
            FOREIGN KEY (IdCategoria) REFERENCES Categorias(Id)
        );
        ");

        var pessoasExistem = connection.ExecuteScalar<int>(
            "SELECT COUNT(1) FROM Pessoas"
        );

        if (pessoasExistem > 0)
            return;

        // Cria pessoas
        connection.Execute(@"
        INSERT INTO Pessoas (Nome, Idade)
        VALUES 
            ('Renata', 22),
            ('Miguel', 17);
        ");

        // Cria categorias
        connection.Execute(@"
        INSERT INTO Categorias (Descricao, Finalidade)
        VALUES 
            ('Alimentação', 1),
            ('Salário', 2),
            ('Lazer', 3);
        ");

        // Cria transações
        connection.Execute(@"
        INSERT INTO Transacoes (Descricao, Valor, Tipo, IdPessoa, IdCategoria)
        VALUES
            ('Salário', 2500.00, 2, 1, 2),
            ('Mercado', 320.75, 1, 1, 1),
            ('Cinema', 50.00, 1, 2, 3);
        ");
    }
}
