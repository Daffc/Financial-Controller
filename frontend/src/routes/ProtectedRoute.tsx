import { Navigate } from "react-router-dom";
import { useAuth } from "../app/authProvider";

export function ProtectedRoute({ children }: any) {
    const { token } = useAuth();

    if (!token) {
        return <Navigate to="/login" />;
    }

    return children;
}