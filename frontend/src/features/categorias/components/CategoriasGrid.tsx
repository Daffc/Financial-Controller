import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCategorias } from "../hooks/useCategorias";
import { finalidadeLabels } from "../../../domain/mappers/finalidadeMapper";
import { type Categoria } from "../../../domain/models/Categoria";

export function CategoriasGrid() {

    const { data, isLoading } = useCategorias();
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
            renderCell: (params) =>
                finalidadeLabels[params.row.finalidade] ?? "Desconhecido",
        }
    ];

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