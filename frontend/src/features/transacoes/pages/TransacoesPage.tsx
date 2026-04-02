import { useEffect, useState } from "react";
import { Box, Paper, Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { TransacoesGrid } from "../components/TransacoesGrid";
import { CreateTransacaoDialog } from "../components/CreateTransacaoDialog";
import { useTransacoes } from "../hooks/useTransacoes";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";

export function TransacoesPage() {

  const { showToast } = useToast();

  const [openCreate, setOpenCreate] = useState(false);
  const { data, isLoading, error, errorUpdatedAt, deleteTransacao } = useTransacoes();

  function deleteTransacaoHandler(id: string) {
    deleteTransacao(id, {
      onSuccess: () => showToast("Removido", "success"),
      onError: (err) => showToast(extractApiError(err), "error"),
    })
  }

  useEffect(() => {
      if (error) {
          showToast(extractApiError(error), "error");
      }
  }, [errorUpdatedAt]);

  return (
    <Box>
      <Box display="flex" justifyContent="flex-end" mb={2}>
        <Button
          startIcon={<AddIcon />}
          onClick={() => setOpenCreate(true)}
        >
          Nova Transação
        </Button>
      </Box>
      <Paper sx={{ p: 2 }}>
        <TransacoesGrid
          data={data}
          isLoading={isLoading}
          onDelete={deleteTransacaoHandler}
        />
      </Paper>
      <CreateTransacaoDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
      />
    </Box>
  );
}