import { useQuery } from "@tanstack/react-query";
import { listCategorias } from "../api/categoria.api";

export function useCategorias() {
    const query = useQuery({
        queryKey: ["categorias"],
        queryFn: listCategorias,
    });

    return {
        ...query,
    };
}