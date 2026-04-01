import { useState } from "react";
import { Box, Paper, Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { CategoriasGrid } from "../components/CategoriasGrid";
import { CreateCategoriaDialog } from "../components/CreateCategoriaDialog";

export function CategoriasPage() {

  const [openCreate, setOpenCreate] = useState(false);

  return (
    <Box>
      <Box display="flex" justifyContent="flex-end" mb={2}>
        <Button
          startIcon={<AddIcon />}
          onClick={() => setOpenCreate(true)}
        >
          Nova Categoria
        </Button>
      </Box>
      <Paper sx={{ p: 2 }}>
        <CategoriasGrid />
      </Paper>
      <CreateCategoriaDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
      />
    </Box>
  );
}