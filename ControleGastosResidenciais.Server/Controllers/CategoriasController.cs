using ControleGastosResidenciais.Server.Models;
using ControleGastosResidenciais.Server.Services;
using Microsoft.AspNetCore.Mvc;

// Controller de Categorias

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    // Obtenção de listagem de categorias criadas
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.ObterCategorias());
    }

    // Criação de categorias, que são utilizadas dentro das transações
    [HttpPost]
    public IActionResult Post([FromBody] Categoria categoria)
    {
        try
        {
            return CreatedAtAction(
                nameof(Get),
                new { id = categoria.Id },
                _service.CriarCategoria(categoria)
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
