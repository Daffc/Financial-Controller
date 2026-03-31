import { useState } from "react";
import {
    AppBar,
    Toolbar,
    Typography,
    Button,
    Box,
    Avatar,
    IconButton,
    Menu,
    MenuItem
} from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
import PersonIcon from '@mui/icons-material/Person';
import { useAuth } from "../app/auth-provider";

export function Navbar() {
    const navigate = useNavigate();
    const location = useLocation();
    const { logout } = useAuth();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    function isActive(path: string) {
        return location.pathname.startsWith(path);
    }

    function handleOpenMenu(event: React.MouseEvent<HTMLElement>) {
        setAnchorEl(event.currentTarget);
    }

    function handleCloseMenu() {
        setAnchorEl(null);
    }

    function handleLogout() {
        handleCloseMenu();
        logout();
        navigate("/login");
    }

    return (
        <AppBar position="static">
            <Toolbar sx={{ justifyContent: "space-between" }}>
                <Typography variant="h6" sx={{ flexGrow: 1 }}>
                    Financial Controller
                </Typography>
                <Box
                    sx={{
                        position: "absolute",
                        left: "50%",
                        transform: "translateX(-50%)",
                        display: "flex",
                        gap: 10
                    }}
                >
                    <Button
                        color="inherit"
                        variant={isActive("/dashboard") ? "outlined" : "text"}
                        onClick={() => navigate("/dashboard")}
                    >
                        Dashboard
                    </Button>
                    <Button
                        color="inherit"
                        variant={isActive("/pessoas") ? "outlined" : "text"}
                        onClick={() => navigate("/pessoas")}
                    >
                        Pessoas
                    </Button>
                    <Button
                        color="inherit"
                        variant={isActive("/categorias") ? "outlined" : "text"}
                        onClick={() => navigate("/categorias")}
                    >
                        Categorias
                    </Button>
                    <Button
                        color="inherit"
                        variant={isActive("/transacoes") ? "outlined" : "text"}
                        onClick={() => navigate("/transacoes")}
                    >
                        Tlogoutransações
                    </Button>

                </Box>
                <Box>
                    <IconButton onClick={handleOpenMenu}>
                        <Avatar sx={{ width: 32, height: 32 }}>
                            <PersonIcon />
                        </Avatar>
                    </IconButton>

                    <Menu
                        anchorEl={anchorEl}
                        open={open}
                        onClose={handleCloseMenu}
                        anchorOrigin={{
                            vertical: "bottom",
                            horizontal: "right"
                        }}
                        transformOrigin={{
                            vertical: "top",
                            horizontal: "right"
                        }}
                    >
                        <MenuItem onClick={handleLogout}>
                            Logout
                        </MenuItem>
                    </Menu>
                </Box>
            </Toolbar>
        </AppBar>
    )
}