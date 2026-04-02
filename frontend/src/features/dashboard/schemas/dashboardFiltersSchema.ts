import { z } from "zod";

export const dashboardFiltersSchema = z.object({
    dataInicio: z
        .string()
        .min(1, "Data início é obrigatória")
        .refine((val) => !isNaN(Date.parse(val)), {
            error: "Data início inválida",
        }),

    dataFim: z
        .string()
        .min(1, "Data fim é obrigatória")
        .refine((val) => !isNaN(Date.parse(val)), {
            error: "Data fim inválida",
        }),
}).superRefine((data, ctx) =>{
    if(new Date(data.dataInicio) > new Date(data.dataFim))
    {
        ctx.addIssue({
            code: "custom",
            message: "Data início não pode ser maior que data fim",
            path: ["dataInicio"],
        });
        ctx.addIssue({
            code: "custom",
            message: "Data início não pode ser maior que data fim",
            path: ["dataFim"],
        });
    }
});

export type DashboardFiltersInput = z.input<typeof dashboardFiltersSchema>;
export type DashboardFiltersData = z.output<typeof dashboardFiltersSchema>;