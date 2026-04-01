import { useState } from "react";
import {
    Box,
    Card,
    CardContent,
    Tab,
    Tabs
} from "@mui/material";
import { RegisterForm } from "../components/RegisterForm";
import { LoginForm } from "../components/LoginForm";
import { useTheme } from "@mui/material/styles";

export function LoginPage() {
    const [tab, setTab] = useState(0);
    const theme = useTheme();

    return (
        <Box
            sx={{
                minHeight: "100vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                backgroundColor: theme.palette.background.default,
            }}
        >
            <Card
                sx={{
                    width: 420,
                    p: 2,
                    backgroundColor: "var(--surface)",
                }}
            >
                <CardContent>
                    <Tabs
                        value={tab}
                        onChange={(_, v) => setTab(v)}
                        centered
                        sx={{
                            mb: 2,
                        }}
                    >
                        <Tab label="Login" />
                        <Tab label="Cadastro" />
                    </Tabs>
                    {tab === 0 && <LoginForm />}
                    {tab === 1 && <RegisterForm setTab={setTab} />}
                </CardContent>
            </Card>
        </Box>
    )
}