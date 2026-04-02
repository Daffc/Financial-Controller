import { TipoTransacao } from "../enums/TipoTransacao"

export const tipoTransacaoLabels: Record<TipoTransacao, string> = {
    [TipoTransacao.DESPESA]: "Despesa",
    [TipoTransacao.RECEITA]: "Receita",
}