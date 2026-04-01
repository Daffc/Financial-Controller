import { z } from "zod";

export const createPessoaSchema = z.object({
    nome: z
        .string()
        .min(1, "Nome é obrigatório")
        .max(200, "Nome deve conter no máximo 200 caracteres"),

    idade: z
        .union([z.number(), z.undefined()])
        .refine((val) => val !== undefined, {
            message: "Idade é obrigatória",
        })
        .refine((val) => typeof val === "number" && !isNaN(val), {
            message: "Idade deve ser um número",
        })
        .refine((val) => val! >= 0, {
            message: "Idade deve ser no mínimo 0",
        })
        .refine((val) => val! <= 150, {
            message: "Idade deve ser no máximo 150",
        }),
});

export type CreatePessoaFormInput = z.input<typeof createPessoaSchema>;
export type CreatePessoaFormData = z.output<typeof createPessoaSchema>;