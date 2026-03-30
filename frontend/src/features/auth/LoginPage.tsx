import { useState } from "react";
import {
    Box,
    Card,
    CardContent,
    Tab,
    Tabs
} from "@mui/material";
import { RegisterForm } from "./components/RegisterForm ";
import { LoginForm } from "./components/LoginForm";

export function LoginPage() {
    const [tab, setTab] = useState(0);

    return (
        <Box
            sx={{
                height: "100vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                backgroundColor: "#f5f5f5",
            }}
        >
            <Card sx={{ width: 400, p: 2 }}>
                <CardContent>
                    <Tabs
                        value={tab}
                        onChange={(_, v) => setTab(v)}
                        centered
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