import type { TipoTransacao } from "../../../domain/enums/TipoTransacao";

export interface CreateTransacaoResponse {
    id: string;
    descricao: string;
    valor: number;
    data: string;
    tipo: TipoTransacao;
}