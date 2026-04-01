import { Box, Container } from "@mui/material";
import { Outlet } from "react-router-dom";
import { Navbar } from "../shared/components/Navbar";

export function MainLayout() {
  return (
    <Box>
      <Navbar />
      <Container sx={{ mt: 4 }}>
        <Outlet />
      </Container>
    </Box>
  );
}