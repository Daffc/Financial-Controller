import { createBrowserRouter } from "react-router-dom";
import { LoginPage } from "../features/auth/LoginPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { RootRedirect } from "../app/RootRedirect";
import { MainLayout } from "../layout/MainLayout";
import { DashboardPage } from "../pages/dashboard/DashboardPage";
import { CategoriasPage } from "../pages/categorias/CategoriasPage";
import { PessoasPage } from "../pages/pessoas/PessoasPage";
import { TransacoesPage } from "../pages/transacoes/TransacoesPage";

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