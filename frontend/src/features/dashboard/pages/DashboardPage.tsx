import { useEffect, useState } from "react";
import { Box, Paper } from "@mui/material";
import { DashboardFilters } from "../components/DashboardFilters";
import type { DashboardFiltersInput } from "../schemas/dashboardFiltersSchema";
import { useTransacoes } from "../../transacoes/hooks/useTransacoes";
import { TransacoesGrid } from "../../transacoes/components/TransacoesGrid";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { TransacoesBarChart } from "../components/TransacoesBarChart";

export function DashboardPage() {
    const [filters, setFilters] = useState<DashboardFiltersInput>();
    const { data, isLoading, error, errorUpdatedAt } = useTransacoes(filters);
    const { showToast } = useToast()

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
                <TransacoesBarChart 
                    data={data} 
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