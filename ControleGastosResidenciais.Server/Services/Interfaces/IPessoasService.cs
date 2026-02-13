using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Services;

// Interface de serviço de pessoa, retornando a obtenção de pessoas, a criação de pessoas, exclusão de pessoas e a edição das mesmas
public interface IPessoasService
{
    IEnumerable<Pessoa> ObterPessoas();
    Pessoa CriarPessoa(Pessoa pessoa);

    void RemoverPessoa(int id);

    Task AtualizarAsync(int id, Pessoa pessoa);
}


