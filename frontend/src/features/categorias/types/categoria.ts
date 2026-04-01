import type { FinalidadeCategoria } from "../../../domain/enums/FinalidadeCategoria";

export interface Categoria {
    id: string;
    descricao: string;
    finalidade: FinalidadeCategoria;
}