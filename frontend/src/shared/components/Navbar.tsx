import { useState } from "react";
import {
    AppBar,
    Toolbar,
    Typography,
    Box,
    Avatar,
    IconButton,
    Menu,
    MenuItem,
    Tabs,
    Tab
} from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
import PersonIcon from "@mui/icons-material/Person";
import { useAuth } from "../../app/auth-provider";

export function Navbar() {
    const navigate = useNavigate();
    const location = useLocation();
    const { logout } = useAuth();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

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

    // 🔥 define aba ativa baseada na rota
    function getTabValue() {
        if (location.pathname.startsWith("/dashboard")) return "/dashboard";
        if (location.pathname.startsWith("/pessoas")) return "/pessoas";
        if (location.pathname.startsWith("/categorias")) return "/categorias";
        if (location.pathname.startsWith("/transacoes")) return "/transacoes";
        return false;
    }

    return (
        <AppBar 
            position="static"
            color="transparent"
            elevation={0}
            sx={{
                backgroundColor: "var(--bg)",
                borderBottom: "1px solid var(--border)"
            }}
        >
            <Toolbar sx={{
                position: "relative",
            }}>
                <Typography variant="h6" sx={{
                    flexGrow: 1,
                    color: "var(--accent)"
                }}>
                    Financial Controller
                </Typography>

                <Tabs
                    value={getTabValue()}
                    onChange={(_, value) => navigate(value)}
                    sx={{
                        position: "absolute",
                        left: "50%",
                        transform: "translateX(-50%)",

                        "& .MuiTabs-indicator": {
                            height: 3,
                            borderRadius: 2
                        },

                        "& .MuiTab-root:hover": {
                            opacity: 0.8
                        }
                    }}
                >
                    <Tab label="Dashboard" value="/dashboard" />
                    <Tab label="Pessoas" value="/pessoas" />
                    <Tab label="Categorias" value="/categorias" />
                    <Tab label="Transações" value="/transacoes" />
                </Tabs>
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
    );
}