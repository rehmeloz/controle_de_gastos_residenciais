import { api } from "./http";
import { Categoria, FinalidadeCategoria } from "../models/Categoria";

// Definindo os endpoints relacionados a categoria (listas e criar)
export async function listarCategorias(): Promise<Categoria[]> {
    const response = await api.get("/categorias");
    return response.data;
}

export async function criarCategoria(categoria: {
    descricao: string;
    finalidade: FinalidadeCategoria;
}) {
    await api.post("/categorias", categoria);
}
