import { api } from "../../../api/axios";
import { type CreateTransacaoRequest } from "../types/createTransacaoRequest";
import { type CreateTransacaoResponse } from "../types/createTransacaoResponse";
import { type ListTransacoesResponse } from "../types/listTransacoesResponse";

export async function listTransacoes(): Promise<ListTransacoesResponse[]> {
    const response = await api.get("/transacoes");
    return response.data;
}

export async function deleteTransacao(id: string): Promise<void> {
    await api.delete(`/transacoes/${id}`);
}

export async function createTransacao(data: CreateTransacaoRequest): Promise<CreateTransacaoResponse> {
    const res = await api.post("/transacoes", data);
    return res.data;
}