import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "@mui/material";
import { AuthProvider } from "./authProvider";
import { theme } from "../styles/theme";
import { FeedbackProvider } from "./feedbackProvider";
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