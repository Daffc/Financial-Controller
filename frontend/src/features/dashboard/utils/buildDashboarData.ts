import { TipoTransacao } from "../../../domain/enums/TipoTransacao";
import { type ListTransacoesResponse } from "../../transacoes/types/listTransacoesResponse";
import { type DashboardData, type CategoriaSummary } from "../types/dashboardData";


export function buildDashboardData(transacoes: ListTransacoesResponse[]): DashboardData {
    const categoriasMap = new Map<string, CategoriaSummary>();
    let totalDespesa = 0;
    let totalReceita = 0;

    for (const t of transacoes) {
        const categoria = t.categoriaDescricao;

        if (!categoriasMap.has(categoria)) {
            categoriasMap.set(categoria, {
                categoria,
                despesa: 0,
                receita: 0
            })
        }

        const item = categoriasMap.get(categoria)!;

        if (t.tipo === TipoTransacao.DESPESA) {
            item.despesa += t.valor;
            totalDespesa += t.valor;
        } else {
            item.receita += t.valor;
            totalReceita += t.valor;
        }
    }

    return {
        categorias: Array.from(categoriasMap.values()),
        summary: {
            totalReceita,
            totalDespesa,
            balanco: totalReceita - totalDespesa,
        },
    };
}