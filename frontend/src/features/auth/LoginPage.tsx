import { useState } from "react";
import { login } from "./api";
import { useAuth } from "../../app/auth-provider";
import { useNavigate } from "react-router-dom";

export function LoginPage() {
    const { login: setAuth } = useAuth();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [error, setError] = useState("");

    async function handleSubmit() {
        try {
            const res = await login({ email, senha });
            setAuth(res.token);
            navigate("/");
        } catch (error) {
            setError("Credenciais inválidas");
        }
    }

    return (
        <div>
            <h2>Login</h2>
            <input
                placeholder="Email"
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Senha"
                onChange={(e) => setSenha(e.target.value)}
            />
            <button onClick={handleSubmit}> Entrar </button>
            {error && <p>{error}</p>}
        </div>
    )
}