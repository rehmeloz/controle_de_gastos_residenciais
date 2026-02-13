// Definindo finalidade de categorias
export enum FinalidadeCategoria {
    Despesa = 1,
    Receita = 2,
    Ambas = 3
}

// Interface de categorias
export interface Categoria {
    id: number;
    descricao: string;
    finalidade: FinalidadeCategoria;
}
