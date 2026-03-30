import { Navigate } from "react-router-dom";
import { useAuth } from "../app/auth-provider";

export function ProtectedRoute({ children }: any) {
    const { token } = useAuth();

    if (!token) {
        return <Navigate to="/login" />;
    }

    return children;
}