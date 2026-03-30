import { z } from "zod";

export const registerUsuarioSchema = z.object({
    nome: z
        .string()
        .min(1, "Nome é obrigatório")
        .max(200, "Nome deve conter no máximo 200 caracteres"),

    email: z
        .email("Formato de email Inválido")
        .max(255, "Email deve conter no máximo 255 caracteres"),

    senha: z
        .string()
        .min(8, "A senha deve ter no mínimo 8 caracteres")
        .max(20, "A senha deve ter no máximo 20 caracteres")
        .regex(/[a-z]/, "Deve conter pelo menos 1 letra minúscula")
        .regex(/[A-Z]/, "Deve conter pelo menos 1 letra maiúscula")
        .regex(/\d/, "Deve conter pelo menos 1 número")
        .regex(/[^a-zA-Z0-9]/, "Deve conter pelo menos 1 caractere especial"),
})
export type RegisterUsuarioFormData = z.infer<typeof registerUsuarioSchema>;