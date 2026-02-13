import { useState } from "react";
import { RelatorioCategorias } from "./RelatorioCategorias";
import { RelatorioPessoas } from "./RelatorioPessoas";
import "../../styles/relatorios.css";

// Função responsável pela seleção/visualização de relatório
export function Relatorios() {
    const [tipoRelatorio, setTipoRelatorio] = useState<"categoria" | "pessoa">("categoria");

    return (
        <div className="relatorio-container">
            <h1>Relatórios</h1>

            <div className="relatorio-header">
                <label>Selecione o relatório:</label>

                <select
                    value={tipoRelatorio}
                    onChange={(e) =>
                        setTipoRelatorio(e.target.value as "categoria" | "pessoa")
                    }
                >
                    <option value="categoria">Por Categoria</option>
                    <option value="pessoa">Por Pessoa</option>
                </select>
            </div>

            {tipoRelatorio === "categoria" && <RelatorioCategorias />}
            {tipoRelatorio === "pessoa" && <RelatorioPessoas />}
        </div>
    );
}
