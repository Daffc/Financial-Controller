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
import { zodResolver } from "@hookform/resolvers/zod";
import { loginUsuarioSchema, type LoginUsuarioFormData } from "../validation/login-usuario-schema";
import { useForm } from "react-hook-form";

export function LoginForm() {
    const { showToast } = useToast();
    const { login: setAuth } = useAuth();
    const navigate = useNavigate();

    const {
        register: formLogin,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<LoginUsuarioFormData>({
        resolver: zodResolver(loginUsuarioSchema)
    })

    async function onSubmit(data: LoginUsuarioFormData) {
        try {
            const res = await login(data);
            setAuth(res.token);
            showToast("Login realizado com sucesso", "success");
            navigate("/");
        } catch (err: any) {
            const msg = extractApiError(err);
            showToast(msg, "error");
        }
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <Stack spacing={2} mt={2}>
                <TextField
                    label="Email"
                    fullWidth
                    {...formLogin("email")}
                    error={!!errors.email}
                    helperText={errors.email?.message}
                />
                <TextField
                    label="Senha"
                    type="password"
                    fullWidth
                    {...formLogin("senha")}
                    error={!!errors.senha}
                    helperText={errors.senha?.message}
                />
                <Button
                    type="submit"
                    variant="contained"
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? "Entrando..." : "Entrar"}
                </Button>
            </Stack>
        </form>
    )
}