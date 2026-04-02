import type { TipoTransacao } from "../enums/TipoTransacao";

export interface Transacao {
    id: string;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    data: string;

    pessoaId: string;
    categoriaId: string;
}