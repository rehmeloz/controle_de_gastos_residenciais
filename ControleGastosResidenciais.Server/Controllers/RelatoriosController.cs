using Microsoft.AspNetCore.Mvc;

// Controller de Relatórios (Consultas)

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatoriosController(IRelatorioService service)
    {
        _service = service;
    }

    // Obtenção de total por pessoa, este dado é utilizado no relatório de consulta de totais por pessoa
    [HttpGet("totais-por-pessoa")]
    public IActionResult GetTotaisPorPessoa()
    {
        return Ok(_service.ObterTotaisPorPessoa());
    }

    // Obtenção do total por categoria, este dado é utilizado no relatório de consulta de totais por categoria
    [HttpGet("totais-por-categoria")]
    public IActionResult GetTotaisPorCategoria()
    {
        return Ok(_service.ObterTotaisPorCategoria());
    }
}
