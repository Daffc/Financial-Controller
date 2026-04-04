import { api } from "../../../api/axios";
import { type CreatePessoaRequest } from "../types/createPessoasRequest";
import { type CreatePessoaResponse } from "../types/createPessoaResponse";
import { type ListPessoasResponse } from "../types/listPessoasResponse";
import { type UpdatePessoaRequest } from "../types/updatePessoasRequest";

export async function listPessoas(): Promise<ListPessoasResponse[]> {
    const response = await api.get("/pessoas");
    return response.data;
}

export async function deletePessoa(id: string): Promise<void> {
    await api.delete(`/pessoas/${id}`);
}

export async function createPessoa(data: CreatePessoaRequest): Promise<CreatePessoaResponse> {
    const res = await api.post("/pessoas", data);
    return res.data;
}

export async function updatePessoa(data: UpdatePessoaRequest): Promise<void> {
    const res = await api.put(`/pessoas/${data.id}`, data);
    return res.data;
}