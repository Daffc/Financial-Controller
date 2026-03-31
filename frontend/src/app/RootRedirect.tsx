import { Navigate } from "react-router-dom";

function isAuthenticated() {
    return !!localStorage.getItem("token");
}

export function RootRedirect(){
    if(isAuthenticated()){
        return <Navigate to="/dashboard" replace />
    }
    return <Navigate to="/login" replace />
}