
import { register } from "../api/registerApi";
import {
    Button,
    Stack,
    TextField
} from "@mui/material";
import { useToast } from "../../../app/feedback-provider";
import { extractApiError } from "../../../api/interceptors";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { registerUsuarioSchema, type RegisterUsuarioFormData } from "../validation/register-usuario-schema";

export function RegisterForm({ setTab }: any) {
    const { showToast } = useToast();
    const {
        register: formRegister,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<RegisterUsuarioFormData>({
        resolver: zodResolver(registerUsuarioSchema),
    });

    async function onSubmit(data: RegisterUsuarioFormData) {
        try {
            await register(data);
            showToast("Usuário cadastrado com sucesso", "success");
            setTab(0);
        } catch (err: any) {
            const msg = extractApiError(err);
            showToast(msg, "error");
        }
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <Stack spacing={2} mt={2}>
                <TextField
                    label="Nome"
                    fullWidth
                    {...formRegister("nome")}
                    error={!!errors.nome}
                    helperText={errors.nome?.message}
                />
                <TextField
                    label="Email"
                    fullWidth
                    {...formRegister("email")}
                    error={!!errors.email}
                    helperText={errors.email?.message}
                />
                <TextField
                    label="Senha"
                    type="password"
                    fullWidth
                    {...formRegister("senha")}
                    error={!!errors.senha}
                    helperText={errors.senha?.message}
                />
                <Button
                    type="submit"
                    variant="contained"
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? "Cadastrando..." : "Cadastrar"}
                </Button>
            </Stack>
        </form>
    )
}