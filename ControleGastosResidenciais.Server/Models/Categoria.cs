namespace ControleGastosResidenciais.Server.Models;

using ControleGastosResidenciais.Server.Enums;

// Classe categoria, possuindo propriedades de identificação, descrição da categoria e a finalidade da mesma
public class Categoria
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
    public ICollection<Transacao> Transacao { get; set; }
}
