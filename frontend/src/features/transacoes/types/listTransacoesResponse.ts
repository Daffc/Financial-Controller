import type { TipoTransacao } from "../../../domain/enums/TipoTransacao";

export interface ListTransacoesResponse {
    id: string,
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    data: string;
    pessoaNome: string;
    categoriaDescricao: string;
}