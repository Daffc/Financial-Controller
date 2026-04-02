import { TextField, Button, Box } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { dashboardFiltersSchema, type DashboardFiltersInput } from "../schemas/dashboardFiltersSchema";
import { useDashboardFilters } from "../hooks/useDashboardFilters";
import { useEffect } from "react";

interface Props {
    onChange: (filters: DashboardFiltersInput) => void;
}

export function DashboardFilters({ onChange }: Props) {
    const { defaultValues } = useDashboardFilters();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<DashboardFiltersInput>({
        resolver: zodResolver(dashboardFiltersSchema),
        defaultValues,
    });

    function handleApply(filters: DashboardFiltersInput) {
        onChange(filters);
    }

    useEffect(() => {
        onChange(defaultValues);
    }, []);

    return (
        <Box>
            <form onSubmit={handleSubmit(handleApply)}>
                <Grid container spacing={2} alignItems="stretch">
                    <Grid size={{ xs: 12, md: 5 }}>
                        <TextField
                            type="date"
                            label="Data início"
                            slotProps={{ inputLabel: { shrink: true } }}
                            {...register("dataInicio")}
                            error={!!errors.dataInicio}
                            helperText={errors.dataInicio?.message}
                            fullWidth
                        />
                    </Grid>
                    <Grid size={{ xs: 12, md: 5 }}>
                        <TextField
                            type="date"
                            label="Data fim"
                            slotProps={{ inputLabel: { shrink: true } }}
                            {...register("dataFim")}
                            error={!!errors.dataFim}
                            helperText={errors.dataFim?.message}
                            fullWidth
                        />
                    </Grid>
                    <Grid size={{ xs: 12, md: 2 }}>
                        <Box height="100%" display="flex">
                            <Button
                                type="submit"
                                variant="contained"
                                fullWidth
                                sx={{ height: "100%" }}
                            >
                                Aplicar
                            </Button>
                        </Box>
                    </Grid>
                </Grid>
            </form>
        </Box>
    );
}