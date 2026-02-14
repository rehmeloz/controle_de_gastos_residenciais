// Definindo os tipos de transação
export enum TipoTransacao {
    Despesa = 1,
    Receita = 2
}

// Interface de transação
export interface Transacao {
    id: number;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    idPessoa: number;
    idCategoria: number;
}

