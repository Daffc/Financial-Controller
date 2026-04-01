import { z } from "zod";
import { FinalidadeCategoria } from "../../../domain/enums/FinalidadeCategoria";

const validFinalidades = Object.values(FinalidadeCategoria);

export const createCategoriaSchema = z.object({
    descricao: z
        .string()
        .min(1, "Descricao é obrigatório")
        .max(400, "Nome deve conter no máximo 400 caracteres"),

    finalidade: z
        .union([z.number(), z.undefined()])
        .refine((val) => val !== undefined, {
            message: "Selecione uma finalidade",
        })
        .refine((val) => validFinalidades.includes(val as any), {
            message: "Finalidade inválida",
        })
});

export type CreateCategoriaFormInput = z.input<typeof createCategoriaSchema>;
export type CreateCategoriaFormData = z.output<typeof createCategoriaSchema>;