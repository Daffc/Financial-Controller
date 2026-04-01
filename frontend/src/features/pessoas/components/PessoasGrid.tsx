import { useState } from "react";
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
import { usePessoas } from "../hooks/usePessoas";
import { type Pessoa } from "../../../domain/models/Pessoa";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";

export function PessoasGrid() {
    const { showToast } = useToast();
    
    const { data, isLoading, deletePessoa } = usePessoas();
    const rows = data || [];
    const columns: GridColDef[] = [
        {
            field: "nome",
            headerName: "Nome",
            flex: 1
        },
        {
            field: "idade",
            headerName: "Idade",
            width: 120,
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

    const [openDialog, setOpenDialog] = useState(false);
    const [selectedPessoa, setSelectedPessoa] = useState<Pessoa | null>(null);

    function handleOpenDialog(pessoa: Pessoa) {
        setSelectedPessoa(pessoa);
        setOpenDialog(true);
    }

    function handleCloseDialog() {
        setOpenDialog(false);
        setSelectedPessoa(null);
    }

    function handleConfirmDelete() {
        if (!selectedPessoa) return;

        deletePessoa(selectedPessoa.id, {
            onSuccess: () => {
                showToast("Pessoa removida com sucesso", "success");
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
                            <strong>{selectedPessoa?.nome}</strong>?
                        </span>

                        <span>
                            Esta ação irá excluir todas as <strong>Transações</strong> atreladas a ela.
                        </span>

                        <span>
                            Esta ação não poderá ser desfeita.
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