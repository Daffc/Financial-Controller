import { Typography, Box, Paper } from "@mui/material";
import { PessoasGrid } from "../../features/pessoas/components/PessoasGrid";

export function PessoasPage() {
  return (
    <Box>
      <Paper sx={{ p: 2 }}>
        <PessoasGrid />
      </Paper>
    </Box>
  );
}