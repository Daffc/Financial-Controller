import { useEffect, useMemo, useState } from "react";
import { Box, Paper } from "@mui/material";
import { DashboardFilters } from "../components/DashboardFilters";
import type { DashboardFiltersInput } from "../schemas/dashboardFiltersSchema";
import { useTransacoes } from "../../transacoes/hooks/useTransacoes";
import { TransacoesGrid } from "../../transacoes/components/TransacoesGrid";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { DashboardCategoriasBarChart } from "../components/DashboardCategoriasBarChart";
import { buildDashboardData } from "../utils/buildDashboarData";
import { DashboardSummary } from "../components/DashboardSummary";

export function DashboardPage() {
    const [filters, setFilters] = useState<DashboardFiltersInput>();
    const { data, isLoading, error, errorUpdatedAt } = useTransacoes(filters);
    const { showToast } = useToast()


    const dashboardData = useMemo(
        () => (data ? buildDashboardData(data) : null),
        [data]
    )
    useEffect(() => {
        if (error) {
            showToast(extractApiError(error), "error");
        }
    }, [errorUpdatedAt]);

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            <Paper sx={{ p: 2 }}>
                <DashboardFilters onChange={setFilters} />
            </Paper>
            <Paper sx={{ p: 2 }}>
                <DashboardSummary
                    summary={dashboardData?.summary}
                    isLoading={isLoading}
                />
                <DashboardCategoriasBarChart
                    categorias={dashboardData?.categorias}
                    isLoading={isLoading}
                />
                <TransacoesGrid
                    data={data}
                    isLoading={isLoading}
                />
            </Paper>
        </Box>
    );
}