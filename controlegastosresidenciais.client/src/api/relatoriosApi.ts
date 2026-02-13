import { api } from "./http";
import { RelatorioPorPessoaResponse, RelatorioPorCategoriaResponse } from "../models/Relatorios";

// Definindo os endpoints de relatórios (Totais por pessoa e totais por categoria)
export async function getTotaisPorPessoa(): Promise<RelatorioPorPessoaResponse> {
    const response = await api.get("/relatorios/totais-por-pessoa");
    return response.data;
}

export async function getTotaisPorCategoria(): Promise<RelatorioPorCategoriaResponse> {
    const response = await api.get("/relatorios/totais-por-categoria");
    return response.data;
}


