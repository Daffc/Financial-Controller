import { useState } from "react";
import { Box, Paper, Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { TransacoesGrid } from "../components/TransacoesGrid";
import { CreateTransacaoDialog } from "../components/CreateTransacaoDialog";

export function TransacoesPage() {

  const [openCreate, setOpenCreate] = useState(false);

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
        <TransacoesGrid />
      </Paper>
      <CreateTransacaoDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
      />
    </Box>
  );
}