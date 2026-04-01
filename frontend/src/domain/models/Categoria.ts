import type { FinalidadeCategoria } from "../enums/FinalidadeCategoria";

export interface Categoria {
    id: string;
    descricao: string;
    finalidade: FinalidadeCategoria;
}