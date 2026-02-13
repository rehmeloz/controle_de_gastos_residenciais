import { useEffect, useState } from "react";
import { listarTransacoes, criarTransacao } from "../../api/transacoesApi";
import { listarPessoas } from "../../api/pessoasApi";
import { listarCategorias } from "../../api/categoriasApi";
import { Transacao, TipoTransacao } from "../../models/Transacao";
import { Pessoa } from "../../models/Pessoa";
import { Categoria, FinalidadeCategoria } from "../../models/Categoria";

// Função responsável pelas transações
export function Transacoes()
{
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [categorias, setCategorias] = useState<Categoria[]>([]);
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState<string>("");
    const [tipo, setTipo] = useState<TipoTransacao>(TipoTransacao.Despesa);
    const [pessoaId, setPessoaId] = useState<number>(0);
    const [categoriaId, setCategoriaId] = useState<number>(0);

    const [erro, setErro] = useState("");

    // Atualizar os dados
    async function carregar() {
        const [t, p, c] = await Promise.all([
            listarTransacoes(),
            listarPessoas(),
            listarCategorias()
        ]);

        setTransacoes(t);
        setPessoas(p);
        setCategorias(c);
    }

    useEffect(() => {
        carregar();
    }, []);

    // Função de seleção de pessoa
    function pessoaSelecionada() {
        return pessoas.find(p => p.id === pessoaId);
    }

    // Função para verificar se a pessoa é menor de idade
    function ehMenorDeIdade() {
        const pessoa = pessoaSelecionada();
        return pessoa ? pessoa.idade < 18 : false;
    }

    // Função para filtrar categorias
    function categoriasFiltradas() {
        return categorias.filter(c => {

            if (tipo === TipoTransacao.Despesa) {
                return (
                    c.finalidade === FinalidadeCategoria.Despesa ||
                    c.finalidade === FinalidadeCategoria.Ambas
                );
            }

            if (tipo === TipoTransacao.Receita) {
                return (
                    c.finalidade === FinalidadeCategoria.Receita ||
                    c.finalidade === FinalidadeCategoria.Ambas
                );
            }

            return false;
        });
    }

    // Função para limpar categorias
    function limparCategoriaSeInvalida(novoTipo: TipoTransacao) {
        const categoriaAtual = categorias.find(c => c.id === categoriaId);
        if (!categoriaAtual) return;

        const valida =
            (novoTipo === TipoTransacao.Despesa &&
                (categoriaAtual.finalidade === FinalidadeCategoria.Despesa ||
                    categoriaAtual.finalidade === FinalidadeCategoria.Ambas)) ||

            (novoTipo === TipoTransacao.Receita &&
                (categoriaAtual.finalidade === FinalidadeCategoria.Receita ||
                    categoriaAtual.finalidade === FinalidadeCategoria.Ambas));

        if (!valida) {
            setCategoriaId(0);
        }
    }

    // Formatação de moeda
    function handleValorChange(e: React.ChangeEvent<HTMLInputElement>) {
        const apenasNumeros = e.target.value.replace(/\D/g, "");

        const valorFormatado = (Number(apenasNumeros) / 100).toLocaleString(
            "pt-BR",
            {
                style: "currency",
                currency: "BRL",
            }
        );

        setValor(valorFormatado);
    }

    // Função para salvar com validações
    async function salvar() {
        setErro("");

        if (!descricao.trim()) {
            setErro("Descrição é obrigatória");
            return;
        }

        if (descricao.trim().length > 400) {
            setErro("Descrição deve ter no máximo 400 caracteres");
            return;
        }

        const valorNumerico = Number(valor.replace(/\D/g, "")) / 100;

        if (!valor || valorNumerico <= 0) {
            setErro("Valor deve ser positivo");
            return;
        }

        if (!pessoaId) {
            setErro("Selecione uma pessoa");
            return;
        }

        if (!categoriaId) {
            setErro("Selecione uma categoria");
            return;
        }

        if (ehMenorDeIdade() && tipo === TipoTransacao.Receita) {
            setErro("Menor de idade só pode lançar despesas");
            return;
        }

        await criarTransacao({
            descricao: descricao.trim(),
            valor: valorNumerico,
            tipo,
            idPessoa: pessoaId,
            idCategoria: categoriaId
        });

        setDescricao("");
        setValor("");
        setTipo(TipoTransacao.Despesa);
        setPessoaId(0);
        setCategoriaId(0);

        carregar();
    }

    // JSX
    return (
        <div className="relatorio-container">
            <h1>Realizar Transações</h1>

            <div className="form-pessoa">

                <div className="campo">
                    <label>Descrição</label>
                    <input
                        maxLength={400}
                        value={descricao}
                        onChange={(e) => setDescricao(e.target.value)}
                    />
                </div>

                <div className="campo">
                    <label>Valor</label>
                    <input
                        type="text"
                        value={valor}
                        onChange={handleValorChange}
                        placeholder="R$ 0,00"
                    />
                </div>

                <div className="campo">
                    <label>Pessoa</label>
                    <select
                        value={pessoaId}
                        onChange={(e) => setPessoaId(Number(e.target.value))}
                    >
                        <option value={0}>Selecione</option>
                        {pessoas.map(p => (
                            <option key={p.id} value={p.id}>
                                {p.nome} ({p.idade} anos)
                            </option>
                        ))}
                    </select>
                </div>

                <div className="campo">
                    <label>Tipo</label>
                    <select
                        value={tipo}
                        onChange={(e) => {
                            const novoTipo = Number(e.target.value) as TipoTransacao;
                            setTipo(novoTipo);
                            limparCategoriaSeInvalida(novoTipo);
                        }}
                    >
                        <option value={TipoTransacao.Despesa}>
                            Despesa
                        </option>

                        {!ehMenorDeIdade() && (
                            <option value={TipoTransacao.Receita}>
                                Receita
                            </option>
                        )}
                    </select>
                </div>

                <div className="campo">
                    <label>Categoria</label>
                    <select
                        value={categoriaId}
                        onChange={(e) => setCategoriaId(Number(e.target.value))}
                    >
                        <option value={0}>Selecione</option>

                        {categoriasFiltradas().map(c => (
                            <option key={c.id} value={c.id}>
                                {c.descricao}
                            </option>
                        ))}
                    </select>
                </div>

                {erro && <span className="erro">{erro}</span>}

                <button className="btn-salvar" onClick={salvar}>
                    Salvar
                </button>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Tipo</th>
                        <th>Pessoa</th>
                        <th>Categoria</th>
                    </tr>
                </thead>
                <tbody>
                    {transacoes.map(t => {
                        const pessoa = pessoas.find(p => p.id === t.idPessoa);
                        const categoria = categorias.find(c => c.id === t.idCategoria);

                        return (
                            <tr key={t.id}>
                                <td>{t.descricao}</td>
                                <td>
                                    {t.valor.toLocaleString("pt-BR", {
                                        style: "currency",
                                        currency: "BRL"
                                    })}
                                </td>
                                <td>{TipoTransacao[t.tipo]}</td>
                                <td>{pessoa?.nome}</td>
                                <td>{categoria?.descricao}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}
