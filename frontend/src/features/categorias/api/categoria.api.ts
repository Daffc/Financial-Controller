import { api } from "../../../api/axios";
import { type Categoria } from "../types/categoria";

export async function listCategorias(): Promise<Categoria[]> {
    const response = await api.get("/categorias");
    return response.data;
}

export async function createCategoria(data: {
    descricao: string;
    finalidade: number;
}): Promise<Categoria> {
    const res = await api.post("/categorias", data);
    return res.data;
}