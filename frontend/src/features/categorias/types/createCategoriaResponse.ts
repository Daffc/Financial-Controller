import type { FinalidadeCategoria } from "../../../domain/enums/FinalidadeCategoria";

export interface CreateCategoriaResponse {
    id: string,
    descricao: string;
    finalidade: FinalidadeCategoria;
}