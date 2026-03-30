
import { useState } from "react";
import { register } from "../register-api";
import {
    Button,
    Stack,
    TextField
} from "@mui/material";

export function RegisterForm({ setTab }: any) {
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [loading, setLoading] = useState(false);

    async function handleSubmit() {
        try {
            setLoading(true);
            await register({
                nome,
                email,
                senha
            })

            alert("Usuário criado com sucesso!");

            setTab(0);
        } catch {
            alert("Erro ao cadastrar usuário");
        } finally {
            setLoading(false);
        }
    }

    return (
        <Stack spacing={2} mt={2}>
            <TextField
                label="Nome"
                fullWidth
                onChange={(e) => setNome(e.target.value)}
            />
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
                helperText="8-20 chars, maiúscula, minúscula, número e especial"
            />
            <Button
                variant="contained"
                fullWidth
                onClick={handleSubmit}
                disabled={loading}
            >
                {loading ? "Cadastrando..." : "Cadastrar"}
            </Button>
        </Stack>
    )
}