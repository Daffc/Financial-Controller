import { api } from "../../../api/axios";
import { type CreateCategoriaRequest } from "../types/createCategoriaRequest";
import { type ListCategoriaResponse } from "../types/listCategoriasResponse";
import { type CreateCategoriaResponse } from "../types/createCategoriaResponse";

export async function listCategorias(): Promise<ListCategoriaResponse[]> {
    const res = await api.get("/categorias");
    return res.data;
}

export async function createCategoria(data: CreateCategoriaRequest): Promise<CreateCategoriaResponse> {
    const res = await api.post("/categorias", data);
    return res.data;
}