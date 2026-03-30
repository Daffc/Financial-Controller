import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "@mui/material";
import { AuthProvider } from "./auth-provider";
import { theme } from "../styles/theme";

const queryClient = new QueryClient();

export function Providers({ children }: { children: React.ReactNode }) {
    return (
        <AuthProvider>
            <QueryClientProvider client={queryClient}>
                <ThemeProvider theme={theme}>
                    {children}
                </ThemeProvider>
            </QueryClientProvider>
        </AuthProvider>
    )
}