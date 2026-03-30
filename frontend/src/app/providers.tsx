import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "@mui/material";
import { AuthProvider } from "./auth-provider";
import { theme } from "../styles/theme";
import { FeedbackProvider } from "./feedback-provider";
import { CssBaseline } from "@mui/material";

const queryClient = new QueryClient();

export function Providers({ children }: { children: React.ReactNode }) {
    return (
        <AuthProvider>
            <QueryClientProvider client={queryClient}>
                <ThemeProvider theme={theme}>
                    <CssBaseline />
                    <FeedbackProvider>
                        {children}
                    </FeedbackProvider>
                </ThemeProvider>
            </QueryClientProvider>
        </AuthProvider>
    )
}