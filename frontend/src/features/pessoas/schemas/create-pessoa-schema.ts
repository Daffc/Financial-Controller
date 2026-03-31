import { z } from "zod";

export const createPessoaSchema = z.object({
    nome: z
        .string()
        .min(1, "Nome é obrigatório")
        .max(200, "Nome deve conter no máximo 200 caracteres"),

    idade: z
        .coerce.number({
            message: "Idade é obrigatória",
        })
        .min(0, "Idade deve ser no mínimo 0")
        .max(150, "Idade deve ser no máximo 150"),
});

export type CreatePessoaFormData = z.infer<typeof createPessoaSchema>;