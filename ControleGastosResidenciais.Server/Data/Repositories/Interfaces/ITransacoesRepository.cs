using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Data.Repositories;

// Interface para transações, possuindo um método para obter todas as transações e outro para inserir uma nova transação
public interface ITransacoesRepository
{
    IEnumerable<Transacao> ObterTodas();
    int Inserir(Transacao transacao);
}
