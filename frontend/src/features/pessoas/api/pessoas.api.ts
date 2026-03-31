import { api } from "../../../api/axios";
import { type Pessoa } from "../types/pessoa";

export async function listPessoas(): Promise<Pessoa[]> {
    const response = await api.get("/pessoas");
    return response.data;
}

export async function deletePessoa(id: string): Promise<void> {
    await api.delete(`/pessoas/${id}`);
}

export async function createPessoa(data: {
    nome: string;
    idade: number;
}): Promise<Pessoa> {
    const res = await api.post("/pessoas", data);
    return res.data;
}