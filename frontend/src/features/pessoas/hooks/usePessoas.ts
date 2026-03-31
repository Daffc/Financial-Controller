import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { listPessoas, deletePessoa } from "../api/pessoas.api";

export function usePessoas() {
    const queryClient = useQueryClient();

    const query = useQuery({
        queryKey: ["pessoas"],
        queryFn: listPessoas,
    });

    const deleteMutation = useMutation({
        mutationFn: deletePessoa,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["pessoas"] });
        },
    });

    return {
        ...query,
        deletePessoa: deleteMutation.mutate,
        isDeleting: deleteMutation.isPending,
    };
}