import { useEffect, useState } from "react";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
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
import { useTransacoes } from "../hooks/useTransacoes";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { tipoTransacaoLabels } from "../../../domain/mappers/tipoTransacaoMapper";
import type { ListTransacoesResponse } from "../types/listTransacoesResponse";
import { formatDateBR } from "../../../utils/date";

export function TransacoesGrid() {
    const { showToast } = useToast();

    const { data, isLoading, error, errorUpdatedAt, deleteTransacao } = useTransacoes();
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
            flex: 1
        },
        {
            field: "tipo",
            headerName: "Tipo",
            flex: 1,
            renderCell: (params) =>
                tipoTransacaoLabels[params.row.tipo] ?? "Desconhecido",
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
        {
            field: "actions",
            headerName: "Ação",
            width: 100,
            sortable: false,
            renderCell: (params) => (
                <IconButton
                    color="error"
                    onClick={() => handleOpenDialog(params.row)}
                >
                    <DeleteIconForever />
                </IconButton>
            ),
        }
    ]

    useEffect(() => {
        if (error) {
            showToast(extractApiError(error), "error");
        }
    }, [errorUpdatedAt]);

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
        if (!selectedTransacao) return;

        deleteTransacao(selectedTransacao.id!, {
            onSuccess: () => {
                showToast("Transação removida com sucesso", "success");
            },
            onError: (err) => {
                showToast(extractApiError(err), "error");
            }
        });

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