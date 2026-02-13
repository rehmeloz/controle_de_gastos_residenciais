import { useEffect, useState } from "react";
import { getTotaisPorCategoria } from "../../api/relatoriosApi";
import { TotalPorCategoria, TotalGeral } from "../../models/Relatorios";
import { formatarMoeda } from "../../utils/Formatar";

// Função responsável pelo relatório de totais por categoria
export function RelatorioCategorias() {
    const [categorias, setCategorias] = useState<TotalPorCategoria[]>([]);
    const [totalGeral, setTotalGeral] = useState<TotalGeral | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function carregar() {
            try {
                const data = await getTotaisPorCategoria();
                setCategorias(data.categorias ?? []);
                setTotalGeral(data.totalGeral);
            } catch (error) {
                console.error("Erro ao carregar relatório por categoria", error);
            } finally {
                setLoading(false);
            }
        }

        carregar();
    }, []);

    if (loading) {
        return <p>Carregando relatório por categoria...</p>;
    }

    return (
        <div>
            <h1>Relatório por Categoria</h1>

            <table border={1} cellPadding={8}>
                <thead>
                    <tr>
                        <th>Categoria</th>
                        <th>Receitas</th>
                        <th>Despesas</th>
                        <th>Saldo</th>
                    </tr>
                </thead>
                <tbody>
                    {categorias.map((categoria) => (
                        <tr key={categoria.idCategoria}>
                            <td>{categoria.descricao}</td>
                            <td>{formatarMoeda(categoria.totalReceitas)}</td>
                            <td>{formatarMoeda(categoria.totalDespesas)}</td>
                            <td>
                                {formatarMoeda(
                                    categoria.totalReceitas - categoria.totalDespesas
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
