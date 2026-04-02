import { TipoTransacao } from "../../../domain/enums/TipoTransacao";
import { type ListTransacoesResponse } from "../../transacoes/types/listTransacoesResponse";
import { type CategoriaSummary } from "../types/categoriaSummary";

export function groupByCategoria ( transacoes: ListTransacoesResponse[] ): CategoriaSummary [] {
    const map = new Map<string, CategoriaSummary >();
    for (const t of transacoes){
        const categoria = t.categoriaDescricao;

        if(!map.has(categoria)) {
            map.set(categoria, {
                categoria,
                despesa: 0,
                receita: 0
            })
        }

        const item = map.get(categoria)!;

        if(t.tipo === TipoTransacao.DESPESA){
            item.despesa += t.valor;
        } else {
            item.receita += t.valor;
        }
    }

    return Array.from(map.values());
}