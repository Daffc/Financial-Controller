import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createTransacao } from "../api/transacoes.api";

export function useCreateTransacao() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: createTransacao,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["transacoes"] })
        }
    });
}