namespace ControleGastosResidenciais.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using ControleGastosResidenciais.Server.Models;
using ControleGastosResidenciais.Server.Services;

// Controller de Transações

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacoesService _service;
    public TransacoesController(ITransacoesService service)
    {
        _service = service;
    }

    // Obtenção de listagem de transações realizadas
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.ObterTransacoes());
    }


    // Criação de uma transação
    // Se a pessoa selecionada para realizar a for menor de idade, apenas o tipo despesa é aceita
    // Se o tipo da transação é despesa, não poderá utilizar uma categoria que tenha a finalidade receita
    [HttpPost]
    public IActionResult Post([FromBody] Transacao transacao)
    {
        try
        {
            return CreatedAtAction(
                nameof(Get),
                new { id = transacao.Id },
                _service.CriarTransacao(transacao)
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
