using ControleGastosResidenciais.Server.Enums;
namespace ControleGastosResidenciais.Server.Models;

/* Classe transação, possuindo propriedades de identificação, descrição da transação, o valor, tipo de transação (Despesa ou Receita), 
o ID da pessoa que realizou a transação e o Id da categoria da transação */
public class Transacao
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }

    public int IdPessoa { get; set; }
    public int IdCategoria { get; set; }
}
