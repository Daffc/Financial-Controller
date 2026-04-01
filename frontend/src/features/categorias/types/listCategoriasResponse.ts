import type { FinalidadeCategoria } from "../../../domain/enums/FinalidadeCategoria";

export interface ListCategoriaResponse {
    id: string,
    descricao: string;
    finalidade: FinalidadeCategoria;
}