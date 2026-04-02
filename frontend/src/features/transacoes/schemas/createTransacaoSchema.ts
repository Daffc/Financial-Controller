import { z } from "zod";
import { TipoTransacao } from "../../../domain/enums/TipoTransacao";

const validTiposTransacao = Object.values(TipoTransacao);

export const createTransacaoSchema = z.object({
    descricao: z
        .string()
        .min(1, "Descrição é obrigatória")
        .max(400, "Descrição deve conter no máximo 400 caracteres"),

    valor: z
        .union([z.number(), z.undefined()])
        .refine((val) => val !== undefined, {
            message: "valor é obrigatório",
        })
        .refine((val) => typeof val === "number" && !isNaN(val), {
            message: "Valor deve ser um número",
        })
        .refine((val) => val! >= 0.01, {
            message: "Valor deve ser de no mínimo 0.01",
        }),

    tipo: z
        .union([z.number(), z.undefined()])
        .refine((val) => val !== undefined, {
            message: "Selecione uma finalidade",
        })
        .refine((val) => validTiposTransacao.includes(val as any), {
            message: "Finalidade inválida",
        }),
    data: z
        .string()
        .min(1, "Data é obrigatória")
        .refine((val) => !isNaN(Date.parse(val)), {
            message: "Data inválida"
        }),

    pessoaId: z
        .string()
        .min(1, "Pessoa é obrigatória")
        .uuid("Pessoa inválida"),

    categoriaId: z
        .string()
        .min(1, "Categoria é obrigatória")
        .uuid("Categoria inválida"),
});

export type CreateTransacaoFormInput = z.input<typeof createTransacaoSchema>;
export type CreateTransacaoFormData = z.output<typeof createTransacaoSchema>;