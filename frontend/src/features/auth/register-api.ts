import { api } from "../../api/axios";

export interface RegisterRequest {
    nome: string,
    email: string,
    senha: string,
}

export async function register(data: RegisterRequest){
    const response = await api.post("/usuarios", data);
    return response.data;
}