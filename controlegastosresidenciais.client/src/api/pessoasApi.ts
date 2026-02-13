import { api } from "./http";
import { Pessoa } from "../models/Pessoa";

// Definindo os endpoints relacionados a pessoa (listar, criar, excluir e editar)
export async function listarPessoas(): Promise<Pessoa[]> {
    const response = await api.get("/pessoas");
    return response.data;
}

export async function criarPessoa(pessoa: Omit<Pessoa, "id">) {
    const response = await api.post("/pessoas", {
        nome: pessoa.nome,
        idade: pessoa.idade
    });

    return response.data;
}

export async function excluirPessoa(id: number): Promise<void> {
    await api.delete(`/pessoas/${id}`);
}

export async function atualizarPessoa(id: number, pessoa: { nome: string; idade: number }) {
    await api.put(`/pessoas/${id}`, pessoa);
}


