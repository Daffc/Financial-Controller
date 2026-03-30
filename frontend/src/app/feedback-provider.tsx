import { createContext, useContext, useState } from "react";
import { Snackbar, Alert } from "@mui/material";

type ToastType = "success" | "error" | "info" | "warning";

interface ToastContextType {
    showToast: (message: string, type?: ToastType) => void;
}

const ToastContext = createContext<ToastContextType>(null!);

export function FeedbackProvider({ children }: { children: React.ReactNode }) {
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [type, setType] = useState<ToastType>("info");

    function showToast(msg: string, t: ToastType = "info") {
        setMessage(msg);
        setType(t);
        setOpen(true);
    }

    return (
        <ToastContext.Provider value={{ showToast }}>
            {children}

            <Snackbar
                open={open}
                autoHideDuration={4000}
                onClose={() => setOpen(false)}
                anchorOrigin={{ vertical: "top", horizontal: "right" }}
            >
                <Alert
                    onClose={() => setOpen(false)}
                    severity={type}
                    variant="filled"
                    sx={{ whiteSpace: "pre-line" }}
                >
                    {message}
                </Alert>
            </Snackbar>
        </ToastContext.Provider>
    );
}

export const useToast = () => useContext(ToastContext);