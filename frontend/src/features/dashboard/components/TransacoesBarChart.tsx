import { BarChart } from "@mui/x-charts/BarChart";
import { type ListTransacoesResponse } from "../../transacoes/types/listTransacoesResponse";
import { groupByCategoria } from "../utils/groupByCategoria";
import { formatCurrencyBR } from "../../../utils/currency";

interface Props {
    data?: ListTransacoesResponse[];
    isLoading: boolean
}

export function TransacoesBarChart({ data, isLoading }: Props) {
    const group = data ? groupByCategoria(data) : []

    const categorias = group.map((g) => g.categoria);
    const despesas = group.map((g) => -g.despesa);
    const receitas = group.map((g) => g.receita);

    return (
        <BarChart
            loading={isLoading}
            xAxis={[
                {
                    scaleType: "band",
                    data: categorias
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