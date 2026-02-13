using ControleGastosResidenciais.Server.Data.Repositories;
using ControleGastosResidenciais.Server.Models;

namespace ControleGastosResidenciais.Server.Services;

// Classe de serviço de pessoas que herda de IPessoasService, possuindo algumas validações no momento da criação de uma pessoa
public class PessoasService : IPessoasService
{
    private readonly IPessoasRepository _repository;

    public PessoasService(IPessoasRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Pessoa> ObterPessoas()
    {
        return _repository.ObterTodas();
    }

    public Pessoa CriarPessoa(Pessoa pessoa)
    {
        ValidarPessoa(pessoa);

        pessoa.Id = _repository.Inserir(pessoa);
        return pessoa;
    }

    public void RemoverPessoa(int id)
    {
        _repository.Remover(id);
    }

    public async Task AtualizarAsync(int id, Pessoa pessoa)
    {
        var existente = await _repository.ObterPorIdAsync(id);

        if (existente == null)
            throw new ArgumentException("Pessoa não encontrada");

        ValidarPessoa(pessoa);

        existente.Nome = pessoa.Nome;
        existente.Idade = pessoa.Idade;

        await _repository.AtualizarAsync(existente);
    }

    private void ValidarPessoa(Pessoa pessoa)
    {
        if (string.IsNullOrWhiteSpace(pessoa.Nome))
            throw new ArgumentException("O nome é obrigatório");

        if (pessoa.Nome.Length > 200)
            throw new ArgumentException("O nome deve ter no máximo 200 caracteres");

        if (pessoa.Idade <= 0)
            throw new ArgumentException("A idade deve ser maior ou igual a zero");
    }
}
