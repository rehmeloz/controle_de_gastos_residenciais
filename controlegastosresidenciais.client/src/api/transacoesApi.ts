import { api } from "./http";
import { Transacao, TipoTransacao } from "../models/Transacao";

// Definindo os endpoints de transações (listar e criar)
export async function listarTransacoes(): Promise<Transacao[]> {
    const response = await api.get("/transacoes");
    return response.data;
}

export async function criarTransacao(transacao: {
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    idPessoa: number;
    idCategoria: number;
}) {
    await api.post("/transacoes", transacao);
}
