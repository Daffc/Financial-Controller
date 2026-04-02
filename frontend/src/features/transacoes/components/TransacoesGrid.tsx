import { useState } from "react";
import { DataGrid, type GridColDef, type GridRenderCellParams } from "@mui/x-data-grid";
import {
    IconButton,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    Box
} from "@mui/material";
import DeleteIconForever from "@mui/icons-material/DeleteForever";
import { tipoTransacaoLabels } from "../../../domain/mappers/tipoTransacaoMapper";
import type { ListTransacoesResponse } from "../types/listTransacoesResponse";
import { formatDateBR } from "../../../utils/date";
import { formatCurrencyBR } from "../../../utils/currency";
import { TipoTransacao } from "../../../domain/enums/TipoTransacao";


interface Props {
    data?: ListTransacoesResponse[];
    isLoading: boolean;
    onDelete?: (id: string) => void;
}
export function TransacoesGrid({ data, isLoading = false, onDelete }: Props) {

    const rows = data || [];
    const columns: GridColDef<ListTransacoesResponse>[] = [
        {
            field: "descricao",
            headerName: "Descrição",
            flex: 1
        },
        {
            field: "valor",
            headerName: "Valor",
            flex: 1,
            align: "right",
            headerAlign: "right",
            renderCell: (params) => {
                return (
                    <span
                        style={{
                            color: params.row.tipo === TipoTransacao.DESPESA
                                ? "var(--color-expense)"
                                : "var(--color-income)",
                            fontWeight: 500,
                        }}
                    >
                        {formatCurrencyBR(params.row.valor)}
                    </span>
                );
            },
        },
        {
            field: "tipo",
            headerName: "Tipo",
            flex: 1,
            renderCell: (params) => {
                   return (
                    <span
                        style={{
                            color: params.row.tipo === TipoTransacao.DESPESA
                                ? "var(--color-expense)"
                                : "var(--color-income)",
                            fontWeight: 500,
                        }}
                    >
                        {tipoTransacaoLabels[params.row.tipo] ?? "Desconhecido"}
                    </span>
                );
            },
        },
        {
            field: "data",
            headerName: "Data",
            flex: 1,
            valueFormatter: (value) =>
                value ? formatDateBR(value as string) : ""
        },
        {
            field: "pessoaNome",
            headerName: "Pessoa",
            flex: 1,
        },
        {
            field: "categoriaDescricao",
            headerName: "Categoria",
            flex: 1
        },
        ...(onDelete ? [{
            field: "actions",
            headerName: "Ação",
            width: 100,
            sortable: false,
            renderCell: (params: GridRenderCellParams<ListTransacoesResponse>) => (
                <IconButton
                    color="error"
                    onClick={() => handleOpenDialog(params.row)}
                >
                    <DeleteIconForever />
                </IconButton>
            ),
        }] : [])
    ]

    const [openDialog, setOpenDialog] = useState(false);
    const [selectedTransacao, setSelectedTransacao] = useState<ListTransacoesResponse | null>(null);

    function handleOpenDialog(transacao: ListTransacoesResponse) {
        setSelectedTransacao(transacao);
        setOpenDialog(true);
    }

    function handleCloseDialog() {
        setOpenDialog(false);
        setSelectedTransacao(null);
    }

    function handleConfirmDelete() {
        if (!selectedTransacao || !onDelete)
            return;

        onDelete(selectedTransacao.id!);
        handleCloseDialog();
    }

    return (
        <>
            <div style={{ height: 500, width: "100%" }}>
                <DataGrid
                    rows={rows}
                    columns={columns}
                    loading={isLoading}
                    getRowId={(row) => row.id}
                    disableRowSelectionOnClick
                />
            </div>
            <Dialog open={openDialog} onClose={handleCloseDialog}>
                <DialogTitle>Confirmar exclusão</DialogTitle>

                <DialogContent>
                    <Box display="flex" flexDirection="column" gap={1}>
                        <span>
                            Deseja realmente excluir{" "}
                            <strong>{selectedTransacao?.descricao}</strong>?
                        </span>
                    </Box>
                </DialogContent>
                <DialogActions>
                    <Button
                        variant="outlined"
                        onClick={handleCloseDialog}
                    >
                        Cancelar
                    </Button>
                    <Button
                        color="error"
                        variant="contained"
                        onClick={handleConfirmDelete}
                    >
                        Excluir
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}