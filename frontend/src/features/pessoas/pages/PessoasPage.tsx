import { PessoasGrid } from "../components/PessoasGrid";
import { useState } from "react";
import { Button, Box, Paper } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { PessoaDialog } from "../components/PessoaDialog";

export function PessoasPage() {
  const [openCreate, setOpenCreate] = useState(false);
  return (
    <Box>
      <Box display="flex" justifyContent="flex-end" mb={2}>
        <Button
          startIcon={<AddIcon />}
          onClick={() => setOpenCreate(true)}
        >
          Nova Pessoa
        </Button>
      </Box>
      <Paper sx={{ p: 2 }}>
        <PessoasGrid />
      </Paper>
      <PessoaDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
      />
    </Box>
  );
}