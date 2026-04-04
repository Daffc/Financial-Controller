import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePessoa } from "../api/pessoas.api";

export function useUpdatePessoa() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updatePessoa,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["pessoas"] })
        }
    });
}