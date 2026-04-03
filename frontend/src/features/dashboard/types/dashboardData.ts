export interface CategoriaSummary {
    categoria: string;
    despesa: number;
    receita: number;
}

export interface DashboardSummary {
    totalReceita: number;
    totalDespesa: number;
    balanco: number
}

export interface DashboardData {
    categorias: CategoriaSummary[];
    summary: DashboardSummary;
}