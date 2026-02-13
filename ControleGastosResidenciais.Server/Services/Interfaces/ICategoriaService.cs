using ControleGastosResidenciais.Server.Models;
namespace ControleGastosResidenciais.Server.Services;

// Interface de serviço de categoria, retornando a obtenção de categorias e a criação das mesmas
public interface ICategoriaService
{
    IEnumerable<Categoria> ObterCategorias();
    Categoria CriarCategoria(Categoria categoria);
}
