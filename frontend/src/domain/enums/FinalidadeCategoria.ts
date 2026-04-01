export const FinalidadeCategoria = {
    DESPESA: 1,
    RECEITA: 2,
    AMBAS: 3,
} as const;

export type FinalidadeCategoria =
    typeof FinalidadeCategoria[keyof typeof FinalidadeCategoria];