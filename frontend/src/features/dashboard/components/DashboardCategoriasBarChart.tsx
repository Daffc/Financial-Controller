import { BarChart } from "@mui/x-charts/BarChart";
import { formatCurrencyBR } from "../../../utils/currency";
import type { CategoriaSummary } from "../types/dashboardData";

interface Props {
    categorias?: CategoriaSummary[];
    isLoading: boolean
}

export function DashboardCategoriasBarChart({ categorias, isLoading }: Props) {

    const labels = categorias?.map((g) => g.categoria) ?? [];
    const despesas = categorias?.map((g) => -g.despesa) ?? [];
    const receitas = categorias?.map((g) => g.receita) ?? [];

    return (
        <BarChart
            loading={isLoading}
            xAxis={[
                {
                    scaleType: "band",
                    data: labels
                },
            ]}

            yAxis={[
                {
                    width: 100,
                    position: 'left',
                    valueFormatter: (value: number | null) =>
                        value !== null ? formatCurrencyBR(value) : "",
                },
            ]}

            series={[
                {
                    data: receitas,
                    label: "Receita",
                    color: "var(--color-income)",
                    stack: "total",
                    valueFormatter: (value: number | null) =>
                        value !== null ? formatCurrencyBR(value) : ""
                },
                {
                    data: despesas,
                    label: "Despesa",
                    color: "var(--color-expense)",
                    stack: "total",
                    valueFormatter: (value: number | null) =>
                        value !== null ? formatCurrencyBR(value) : ""
                }
            ]}
            height={400}
        />
    );
}