using ControleGastosResidenciais.Server.Models.Relatorios;
using ControleGastosResidenciais.Server.Data.Repositories;

// Classe de serviço de relatórios que herda de IRelatorioService
public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _repository;

    public RelatorioService(IRelatorioRepository repository)
    {
        _repository = repository;
    }

    public TotaisPorPessoaResponse ObterTotaisPorPessoa()
    {
        return _repository.ObterTotaisPorPessoa();
    }

    public TotaisPorCategoriaResponse ObterTotaisPorCategoria()
    {
        return _repository.ObterTotaisPorCategoria();
    }
}
