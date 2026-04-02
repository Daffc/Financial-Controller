import { api } from "../../../api/axios";
import { type CreateTransacaoRequest } from "../types/createTransacaoRequest";
import { type CreateTransacaoResponse } from "../types/createTransacaoResponse";
import type { ListTransacoesRequest } from "../types/listTransacoesRequest";
import { type ListTransacoesResponse } from "../types/listTransacoesResponse";

export async function listTransacoes( params ?: ListTransacoesRequest): Promise<ListTransacoesResponse[]> {
    const response = await api.get("/transacoes", {
        params
    });
    return response.data;
}

export async function deleteTransacao(id: string): Promise<void> {
    await api.delete(`/transacoes/${id}`);
}

export async function createTransacao(data: CreateTransacaoRequest): Promise<CreateTransacaoResponse> {
    const res = await api.post("/transacoes", data);
    return res.data;
}