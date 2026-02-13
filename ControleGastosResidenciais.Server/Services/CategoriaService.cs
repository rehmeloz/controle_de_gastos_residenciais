using ControleGastosResidenciais.Server.Data.Repositories;
using ControleGastosResidenciais.Server.Enums;
using ControleGastosResidenciais.Server.Models;

namespace ControleGastosResidenciais.Server.Services;

// Classe de serviço de categorias que herda de ICategoriaService, possuindo algumas validações no momento da criação de uma categoria
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Categoria> ObterCategorias()
    {
        return _repository.ObterTodas();
    }

    public Categoria CriarCategoria(Categoria categoria)
    {
        // Validações para criar uma nova categoria
        if (string.IsNullOrWhiteSpace(categoria.Descricao))
            throw new ArgumentException("A descrição é obrigatória");

        if (categoria.Descricao.Length > 400)
            throw new ArgumentException("A descrição deve ter no máximo 400 caracteres");

        if (!Enum.IsDefined(typeof(FinalidadeCategoria), categoria.Finalidade))
            throw new ArgumentException("Finalidade inválida!");

        categoria.Id = _repository.Inserir(categoria);
        return categoria;
    }
}
