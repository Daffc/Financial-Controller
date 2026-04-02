import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { listTransacoes, deleteTransacao } from "../api/transacoes.api";

export function useTransacoes() {
    const queryClient = useQueryClient();

    const query = useQuery({
        queryKey: ["transacoes"],
        queryFn: listTransacoes,
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