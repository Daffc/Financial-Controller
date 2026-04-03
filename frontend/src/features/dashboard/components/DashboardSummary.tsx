// src/features/dashboard/components/DashboardSummary.tsx

import { Box, Typography, Skeleton } from "@mui/material";
import type { DashboardSummary as Summary } from "../types/dashboardData";
import { formatCurrencyBR } from "../../../utils/currency";

interface Props {
    summary?: Summary;
    isLoading?: boolean;
}

export function DashboardSummary({ summary, isLoading }: Props) {
    return (
        <Box display="flex" gap={2}>
            <SummaryCard
                label="Receita"
                value={summary?.totalReceita}
                color="var(--color-income)"
                isLoading={isLoading}
            />

            <SummaryCard
                label="Despesa"
                value={summary?.totalDespesa}
                color="var(--color-expense)"
                isLoading={isLoading}
            />

            <SummaryCard
                label="Saldo"
                value={summary?.balanco}
                color={
                    summary && summary.balanco >= 0
                        ? "var(--color-income)"
                        : "var(--color-expense)"
                }
                isLoading={isLoading}
            />
        </Box>
    );
}

interface SummaryCardProps {
    label: string;
    value?: number;
    color: string;
    isLoading?: boolean;
}

function SummaryCard({ label, value, color, isLoading }: SummaryCardProps) {
    return (
        <Box sx={{ p: 2, display: "flex", flexDirection: "column",flex: 1, textAlign: "center", alignItems: "center"}}>
            <Typography variant="body2" color="text.secondary">
                {label}
            </Typography>
            {isLoading ? (
                <Skeleton width="60%" height={32}  />
            ) : (
                <Typography variant="h6" sx={{ color }}>
                    {value !== undefined ? formatCurrencyBR(value) : "-"}
                </Typography>
            )}
        </Box>
    );
}