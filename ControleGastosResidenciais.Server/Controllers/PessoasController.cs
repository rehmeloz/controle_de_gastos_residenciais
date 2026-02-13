using ControleGastosResidenciais.Server.Models;
using ControleGastosResidenciais.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// Controller de Pessoas

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoasService _service;

    public PessoasController(IPessoasService service)
    {
        _service = service;
    }

    // Obtenção de listagem de pessoas criadas
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.ObterPessoas());
    }

    // Criação de pessoas
    [HttpPost]
    public IActionResult Post([FromBody] Pessoa pessoa)
    {
        try
        {
            return CreatedAtAction(
                nameof(Get),
                new { id = pessoa.Id },
                _service.CriarPessoa(pessoa)
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Exclusão de pessoas (Quando uma pessoa é excluída, as suas transações também serão)
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _service.RemoverPessoa(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Edição de pessoas através do ID, podendo editar o nome e idade
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Pessoa pessoa)
    {
        try
        {
            await _service.AtualizarAsync(id, pessoa);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}