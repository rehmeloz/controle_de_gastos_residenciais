using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Data.Repositories;

// Interface para categorias, possuido um método para obter todas as categorias e outro para inserir uma nova categoria
public interface ICategoriaRepository
{
    IEnumerable<Categoria> ObterTodas();
    int Inserir(Categoria categoria);
}
