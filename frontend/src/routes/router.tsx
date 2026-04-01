import { createBrowserRouter } from "react-router-dom";
import { LoginPage } from "../features/auth/pages/LoginPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { RootRedirect } from "../app/RootRedirect";
import { MainLayout } from "../layout/MainLayout";
import { DashboardPage } from "../features/dashboard/pages/DashboardPage";
import { CategoriasPage } from "../features/categorias/pages/CategoriasPage";
import { PessoasPage } from "../features/pessoas/pages/PessoasPage";
import { TransacoesPage } from "../features/transacoes/pages/TransacoesPage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <RootRedirect />
    },
    {
        path: "/login",
        element: <LoginPage />
    },
    {
        path: "/",
        element: (
            <ProtectedRoute>
                <MainLayout />
            </ProtectedRoute>
        ),
        children: [
            {
                path: "/dashboard",
                element: <DashboardPage />
            },
            {
                path: "/categorias",
                element: <CategoriasPage />
            },
            {
                path: "/pessoas",
                element: <PessoasPage />
            },
            {
                path: "/transacoes",
                element: <TransacoesPage />
            }
        ]
    }
])