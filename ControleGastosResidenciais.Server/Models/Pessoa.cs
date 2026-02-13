namespace ControleGastosResidenciais.Server.Models;

// Classe pessoa, possuindo propriedades de identificação, nome da pessoa e a idade da mesma
public class Pessoa
{
    public int Id { get; set; }      
    public string? Nome { get; set; } 
    public int Idade { get; set; }   
}
