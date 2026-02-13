using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Services;

// Interface de serviço de transações, retonando a obtenção de transações e a criação das mesmas
public interface ITransacoesService
{
    IEnumerable<Transacao> ObterTransacoes();
    Transacao CriarTransacao(Transacao transacao);
}
