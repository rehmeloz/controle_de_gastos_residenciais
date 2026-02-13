import { useEffect, useState } from "react";
import { listarPessoas, criarPessoa, excluirPessoa, atualizarPessoa } from "../../api/pessoasApi";
import { Pessoa } from "../../models/Pessoa";

// Função responsável por criar, listar, excluir e editar pessoas
export function Pessoas() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState("");
    const [erro, setErro] = useState("");
    const [editandoId, setEditandoId] = useState<number | null>(null);

    // Atualiza os dados
    async function carregar() {
        const data = await listarPessoas();
        setPessoas(data);
    }

    useEffect(() => {
        carregar();
    }, []);

    // Função de edição
    function editar(p: Pessoa) {
        setNome(p.nome);
        setIdade(String(p.idade));
        setEditandoId(p.id);
        setErro("");
    }

    // Função para cancelar a edição
    function cancelarEdicao() {
        setNome("");
        setIdade("");
        setEditandoId(null);
        setErro("");
    }

    // Função para salvar com validaçõee
    async function salvar() {
        setErro("");

        const nomeLimpo = nome.trim();

        if (!nomeLimpo) {
            setErro("Nome é obrigatório");
            return;
        }

        if (nomeLimpo.length > 200) {
            setErro("O nome deve ter no máximo 200 caracteres");
            return;
        }

        const regexNome = /^[\p{L}\s]*$/u;

        if (!regexNome.test(nomeLimpo)) {
            setErro("O nome deve conter apenas letras e espaços");
            return;
        }

        const idadeNumero = Number(idade);

        if (!idade || isNaN(idadeNumero) || idadeNumero <= 0) {
            setErro("Idade deve ser um número maior que zero");
            return;
        }

        if (editandoId !== null) {
            await atualizarPessoa(editandoId, {
                nome: nomeLimpo,
                idade: idadeNumero
            });
        } else {
            await criarPessoa({
                nome: nomeLimpo,
                idade: idadeNumero
            });
        }

        setNome("");
        setIdade("");
        setEditandoId(null);
        carregar();
    }

    // Função para excluir
    async function remover(id: number) {
        if (confirm("Deseja excluir esta pessoa?")) {
            try {
                await excluirPessoa(id);
                carregar();
            } catch {
                alert("Erro ao excluir pessoa. Verifique se há transações vinculadas.");
            }
        }
    }

    // JSX
    return (
        <div className="relatorio-container">
            <h1>Cadastro de Pessoas</h1>

            <div className="form-pessoa">

                <div className="campo">
                    <label><strong>Nome</strong></label>
                    <input
                        type="text"
                        maxLength={200}
                        value={nome}
                        onChange={(e) => {
                            const valor = e.target.value;

                            if (/^[\p{L}\s]*$/u.test(valor)) {
                                setNome(valor);
                            }
                        }}
                        placeholder="Digite o nome"
                    />
                </div>

                <div className="campo">
                    <label><strong>Idade</strong></label>
                    <input
                        type="number"
                        min={1}
                        value={idade}
                        onChange={(e) => {
                            const valor = e.target.value;

                            if (valor === "" || Number(valor) > 0) {
                                setIdade(valor);
                            }
                        }}
                        placeholder="Digite a idade"
                    />
                </div>

                {erro && (
                    <span className="erro">
                        {erro}
                    </span>
                )}

                <div style={{ display: "flex", gap: "10px" }}>
                    <button className="btn-salvar" onClick={salvar}>
                        {editandoId !== null ? "Atualizar" : "Salvar"}
                    </button>

                    {editandoId !== null && (
                        <button className="btn-secundario" onClick={cancelarEdicao}>
                            Cancelar
                        </button>
                    )}
                </div>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Idade</th>
                        <th className="col-acoes">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {pessoas.map(p => (
                        <tr
                            key={p.id}
                            style={{
                                backgroundColor: editandoId === p.id ? "#f0f8ff" : "transparent"
                            }}
                        >
                            <td>{p.nome}</td>
                            <td>{p.idade}</td>
                            <td className="col-acoes">
                                <div className="acoes-botoes">
                                    <button
                                        className="btn-acao btn-editar"
                                        onClick={() => editar(p)}
                                    >
                                        Editar
                                    </button>

                                    <button
                                        className="btn-acao btn-excluir"
                                        onClick={() => remover(p.id)}
                                    >
                                        Excluir
                                    </button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}