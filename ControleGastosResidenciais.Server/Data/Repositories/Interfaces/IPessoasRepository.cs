using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Data.Repositories;

// Interface para pessoas, possuido métodos para obter todas as pessoas, inserir uma nova pessoa, excluir uma pessoa e atualizar uma pessoa
public interface IPessoasRepository
{
    IEnumerable<Pessoa> ObterTodas();
    int Inserir(Pessoa pessoa);

    void Remover(int id);

    Task AtualizarAsync(Pessoa pessoa);
    Task<Pessoa?> ObterPorIdAsync(int id);

}
