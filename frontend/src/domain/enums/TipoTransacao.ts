export const TipoTransacao = {
    DESPESA: 1,
    RECEITA: 2,
} as const;

export type TipoTransacao =
    typeof TipoTransacao[keyof typeof TipoTransacao];