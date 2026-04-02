import type { TipoTransacao } from "../../../domain/enums/TipoTransacao";

export interface ListTransacoesRequest {
    dataInicio?:string;
    dataFim?: string;
    tipo?: TipoTransacao;
    pessoaId?: string;
    categoriaId?: string;
}