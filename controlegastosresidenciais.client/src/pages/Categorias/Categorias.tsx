import { useEffect, useState } from "react";
import { listarCategorias, criarCategoria } from "../../api/categoriasApi";
import { Categoria, FinalidadeCategoria } from "../../models/Categoria";

// Função responsável por criar e listas categorias
export function Categorias() {
    const [categorias, setCategorias] = useState<Categoria[]>([]);
    const [descricao, setDescricao] = useState("");
    const [finalidade, setFinalidade] = useState<FinalidadeCategoria>(
        FinalidadeCategoria.Despesa
    );
    const [erro, setErro] = useState("");

    // Atualiza os dados
    async function carregar() {
        const data = await listarCategorias();
        setCategorias(data);
    }

    useEffect(() => {
        carregar();
    }, []);

    // Função para salvar com validações
    async function salvar() {
        setErro("");

        const descricaoLimpa = descricao.trim();

        if (!descricaoLimpa) {
            setErro("Descrição é obrigatória");
            return;
        }

        if (descricaoLimpa.length > 400) {
            setErro("Descrição deve ter no máximo 400 caracteres");
            return;
        }

        await criarCategoria({
            descricao: descricaoLimpa,
            finalidade: finalidade
        });

        setDescricao("");
        setFinalidade(FinalidadeCategoria.Despesa);
        carregar();
    }

    // JSX
    return (
        <div className="relatorio-container">
            <h1>Cadastro de Categorias</h1>

            <div className="form-pessoa">

                <div className="campo">
                    <label><strong>Descrição</strong></label>
                    <input
                        type="text"
                        maxLength={400}
                        value={descricao}
                        onChange={(e) => setDescricao(e.target.value)}
                        placeholder="Digite a descrição"
                    />
                </div>

                <div className="campo">
                    <label><strong>Finalidade</strong></label>
                    <select
                        value={finalidade}
                        onChange={(e) =>
                            setFinalidade(Number(e.target.value) as FinalidadeCategoria)
                        }
                    >
                        <option value={FinalidadeCategoria.Despesa}>Despesa</option>
                        <option value={FinalidadeCategoria.Receita}>Receita</option>
                        <option value={FinalidadeCategoria.Ambas}>Ambas</option>
                    </select>
                </div>

                {erro && (
                    <span className="erro">
                        {erro}
                    </span>
                )}

                <button className="btn-salvar" onClick={salvar}>
                    Salvar
                </button>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Descrição</th>
                        <th>Finalidade</th>
                    </tr>
                </thead>
                <tbody>
                    {categorias.map(c => (
                        <tr key={c.id}>
                            <td>{c.descricao}</td>
                            <td>{FinalidadeCategoria[c.finalidade]}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
