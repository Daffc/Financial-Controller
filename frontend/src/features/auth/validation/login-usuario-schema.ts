import { z } from "zod";

export const loginUsuarioSchema = z.object({
    email: z
        .email("Formato de email Inválido")
        .min(1, "Email é obrigatório"),

    senha: z
        .string()
        .min(1, "A senha é obrigatória'"),
})
export type LoginUsuarioFormData = z.infer<typeof loginUsuarioSchema>;