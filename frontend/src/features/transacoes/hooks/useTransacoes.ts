import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { listTransacoes, deleteTransacao } from "../api/transacoes.api";
import type { ListTransacoesRequest } from "../types/listTransacoesRequest";

export function useTransacoes(filters?: ListTransacoesRequest) {
    const queryClient = useQueryClient();

    const query = useQuery({
        queryKey: ["transacoes", filters],
        queryFn: () => listTransacoes(filters),
    });

    const deleteMutation = useMutation({
        mutationFn: deleteTransacao,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["transacoes"] });
        },
    });

    return {
        ...query,
        deleteTransacao: deleteMutation.mutate,
        isDeleting: deleteMutation.isPending,
    };
}