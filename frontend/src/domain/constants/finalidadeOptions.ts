import { FinalidadeCategoria } from "../enums/FinalidadeCategoria";
import { finalidadeLabels } from "../mappers/finalidadeMapper";

export const finalidadeOptions = Object.values(FinalidadeCategoria).map((value) => ({
    value,
    label: finalidadeLabels[value],
}))