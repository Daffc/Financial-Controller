import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCategoria } from "../api/categoria.api";

export function useCreateCategoria() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: createCategoria,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["categorias"] })
        }
    });
}