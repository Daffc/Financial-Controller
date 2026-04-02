export interface CreateTransacaoRequest {
    descricao: string;
    valor: number;
    tipo: number;
    data: string;
    pessoaId: string;
    categoriaId: string;
}