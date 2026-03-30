
import { useState } from "react";
import { login } from "../login-api";
import { useAuth } from "../../../app/auth-provider";
import { useNavigate } from "react-router-dom";
import {
    Button,
    Stack,
    TextField
} from "@mui/material";
import { extractApiError } from "../../../api/interceptors";
import { useToast } from "../../../app/feedback-provider";

export function LoginForm() {
    const { showToast } = useToast();
    const { login: setAuth } = useAuth();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [loading, setLoading] = useState(false);

    async function handleSubmit() {
        try {
            setLoading(true);
            const res = await login({ email, senha });
            setAuth(res.token);
            showToast("Login realizado com sucesso", "success");
            navigate("/");
        } catch (err: any) {
            const msg = extractApiError(err);
            showToast(msg, "error");
        } finally {
            setLoading(false);
        }
    }

    return (

        <Stack spacing={2} mt={2}>
            <TextField
                label="Email"
                fullWidth
                onChange={(e) => setEmail(e.target.value)}
            />
            <TextField
                label="Senha"
                type="password"
                fullWidth
                onChange={(e) => setSenha(e.target.value)}
            />
            <Button
                variant="contained"
                fullWidth
                onClick={handleSubmit}
                disabled={loading}
            >
                {loading ? "Entrando..." : "Entrar"}
            </Button>
        </Stack>
    )
}