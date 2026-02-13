import { useEffect, useState } from "react";
import { getTotaisPorPessoa } from "../../api/relatoriosApi";
import { TotalPorPessoa, TotalGeral } from "../../models/Relatorios";
import { formatarMoeda } from "../../utils/Formatar";

// Função responsável pelo relatório de totais por pessoa
export function RelatorioPessoas() {
    const [pessoas, setPessoas] = useState<TotalPorPessoa[]>([]);
    const [totalGeral, setTotalGeral] = useState<TotalGeral | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function carregar() {
            try {
                const data = await getTotaisPorPessoa();
                setPessoas(data.pessoas ?? []);
                setTotalGeral(data.totalGeral);
            } catch (error) {
                console.error("Erro ao carregar relatório", error);
            } finally {
                setLoading(false);
            }
        }

        carregar();
    }, []);


    if (loading) {
        return <p>Carregando relatório...</p>;
    }

    return (
        <div>
            <h1>Relatório por Pessoa</h1>

            <table border={1} cellPadding={8}>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Receitas</th>
                        <th>Despesas</th>
                        <th>Saldo</th>
                    </tr>
                </thead>
                <tbody>
                    {pessoas.map((pessoa) => (
                        <tr key={pessoa.idPessoa}>
                            <td>{pessoa.nome}</td>
                            <td>{formatarMoeda(pessoa.totalReceitas)}</td>
                            <td>{formatarMoeda(pessoa.totalDespesas)}</td>
                            <td>
                                {formatarMoeda(
                                    pessoa.totalReceitas - pessoa.totalDespesas
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {totalGeral && (
                <>
                    <div className="total-geral">
                        <h2>Total Geral</h2>
                        <p>Receitas: {formatarMoeda(totalGeral.totalReceitas)}</p>
                        <p>Despesas: {formatarMoeda(totalGeral.totalDespesas)}</p>
                        <p>
                            Saldo: {formatarMoeda(
                                totalGeral.totalReceitas - totalGeral.totalDespesas
                            )}
                        </p>
                    </div>
                </>
            )}
        </div>
    );
}
