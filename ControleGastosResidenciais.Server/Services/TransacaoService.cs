using ControleGastosResidenciais.Server.Data;
using ControleGastosResidenciais.Server.Data.Repositories;
using ControleGastosResidenciais.Server.Enums;
using ControleGastosResidenciais.Server.Models;
using Dapper;

namespace ControleGastosResidenciais.Server.Services;

// Classe de serviço de transações que herda de ITransacoesService, possuindo algumas validações no momento da criação de uma nova transação
public class TransacaoService : ITransacoesService
{
    private readonly ITransacoesRepository _repository;

    public TransacaoService(ITransacoesRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Transacao> ObterTransacoes()
    {
        return _repository.ObterTodas();
    }

    public Transacao CriarTransacao(Transacao transacao)
    {
        // Validações para criar uma nova transação
        if (string.IsNullOrWhiteSpace(transacao.Descricao))
            throw new ArgumentException("A descrição é obrigatória");

        if (transacao.Descricao.Length > 400)
            throw new ArgumentException("A descrição deve ter no máximo 400 caracteres");

        if (transacao.Valor <= 0)
            throw new ArgumentException("O valor deve ser positivo");

        if (!Enum.IsDefined(typeof(TipoTransacao), transacao.Tipo))
            throw new ArgumentException("Tipo de transação inválido");

        using var connection = DatabaseConfig.GetConnection();

        // Valida pessoa
        var idadePessoa = connection.QuerySingleOrDefault<int?>(
            "SELECT Idade FROM Pessoas WHERE Id = @Id",
            new { Id = transacao.IdPessoa }
        );

        if (idadePessoa == null)
            throw new ArgumentException("Pessoa não encontrada");

        if (idadePessoa < 18 && transacao.Tipo == TipoTransacao.Receita)
            throw new ArgumentException("Menores de idade só podem registrar despesas");

        // Valida categoria
        var finalidadeCategoria = connection.QuerySingleOrDefault<int?>(
            "SELECT Finalidade FROM Categorias WHERE Id = @Id",
            new { Id = transacao.IdCategoria }
        );

        if (finalidadeCategoria == null)
            throw new ArgumentException("Categoria não encontrada");

        if (finalidadeCategoria == (int)FinalidadeCategoria.Despesa &&
            transacao.Tipo == TipoTransacao.Receita)
            throw new ArgumentException("Categoria não permite receitas");

        if (finalidadeCategoria == (int)FinalidadeCategoria.Receita &&
            transacao.Tipo == TipoTransacao.Despesa)
            throw new ArgumentException("Categoria não permite despesas");

        transacao.Id = _repository.Inserir(transacao);
        return transacao;
    }
}
