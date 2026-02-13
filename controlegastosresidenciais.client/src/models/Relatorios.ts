// Interfaces para relatórios
export interface TotalPorPessoa {
    idPessoa: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
}

export interface TotalPorCategoria {
    idCategoria: number;
    descricao: string;
    totalReceitas: number;
    totalDespesas: number;
}

export interface TotalGeral {
    totalReceitas: number;
    totalDespesas: number;
}

export interface RelatorioPorPessoaResponse {
    pessoas: TotalPorPessoa[];
    totalGeral: TotalGeral;
}

export interface RelatorioPorCategoriaResponse {
    categorias: TotalPorCategoria[];
    totalGeral: TotalGeral;
}
