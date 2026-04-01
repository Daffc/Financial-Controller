import { api } from "../../../api/axios";
import { type CreateCategoriaRequest } from "../types/CreateCategoriaRequest";
import { type ListCategoriaResponse } from "../types/ListCategoriasResponse";
import { type CreateCategoriaResponse } from "../types/CreateCategoriaResponse";

export async function listCategorias(): Promise<ListCategoriaResponse[]> {
    const res = await api.get("/categorias");
    return res.data;
}

export async function createCategoria(data: CreateCategoriaRequest): Promise<CreateCategoriaResponse> {
    const res = await api.post("/categorias", data);
    return res.data;
}