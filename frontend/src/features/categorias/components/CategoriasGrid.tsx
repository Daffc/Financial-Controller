import { useEffect } from "react";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCategorias } from "../hooks/useCategorias";
import { finalidadeLabels } from "../../../domain/mappers/finalidadeMapper";
import { type Categoria } from "../../../domain/models/Categoria";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { FinalidadeCategoria } from "../../../domain/enums/FinalidadeCategoria";

export function CategoriasGrid() {

    const { showToast } = useToast();
    const { data, isLoading, errorUpdatedAt, error } = useCategorias();
    const rows = data || [];
    const columns: GridColDef<Categoria>[] = [
        {
            field: "descricao",
            headerName: "Descrição",
            flex: 1,
        },
        {
            field: "finalidade",
            headerName: "Finalidade",
            width: 150,
            renderCell: (params) => {
                return (
                    <span
                        style={{
                            color:
                                params.row.finalidade === FinalidadeCategoria.DESPESA
                                    ? "var(--color-expense)"
                                    : params.row.finalidade === FinalidadeCategoria.RECEITA
                                        ? "var(--color-income)"
                                        : "var(--color-neutral)",
                            fontWeight: 500,
                        }}
                    >
                        {finalidadeLabels[params.row.finalidade] ?? "Desconhecido"}
                    </span>
                );
            },
        }
    ];

    useEffect(() => {
        if (error) {
            showToast(extractApiError(error), "error");
        }
    }, [errorUpdatedAt]);

    return (
        <div style={{ height: 500, width: "100%" }}>
            <DataGrid
                rows={rows}
                columns={columns}
                loading={isLoading}
                getRowId={(row) => row.id}
                disableRowSelectionOnClick
            />
        </div>
    );
}