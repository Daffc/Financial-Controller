import { FinalidadeCategoria } from "../enums/FinalidadeCategoria";

export const finalidadeLabels: Record<FinalidadeCategoria, string> = {
    [FinalidadeCategoria.DESPESA]: "Despesa",
    [FinalidadeCategoria.RECEITA]: "Receita",
    [FinalidadeCategoria.AMBAS]: "Ambas"
}