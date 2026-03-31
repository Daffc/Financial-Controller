import { createTheme } from "@mui/material";
import { tokens } from "./tokens";
import { ptBR } from "@mui/x-data-grid/locales";

/**
 * Aplica CSS Variables no :root
 */
function applyCssVariables(mode: "light" | "dark") {
  if (typeof document === "undefined") return; // evita problemas SSR

  const root = document.documentElement;
  const isDark = mode === "dark";

  root.style.setProperty("--bg",
    isDark ? tokens.colors.background.dark : tokens.colors.background.light
  );

  root.style.setProperty("--surface",
    isDark ? tokens.colors.surface.dark : tokens.colors.surface.light
  );

  root.style.setProperty("--text-primary",
    isDark ? tokens.colors.text.primaryDark : tokens.colors.text.primaryLight
  );

  root.style.setProperty("--text-secondary",
    isDark ? tokens.colors.text.secondaryDark : tokens.colors.text.secondaryLight
  );

  root.style.setProperty("--border",
    isDark ? tokens.colors.border.dark : tokens.colors.border.light
  );

  root.style.setProperty("--accent",
    isDark ? tokens.colors.accent.dark : tokens.colors.accent.light
  );

  root.style.setProperty("--code-bg",
    isDark ? tokens.colors.codeBg.dark : tokens.colors.codeBg.light
  );

  root.style.setProperty("--font-family", tokens.typography.fontFamily);
  root.style.setProperty("--font-mono", tokens.typography.mono);
}

/**
 * Criação do theme
 */
export function createAppTheme(mode: "light" | "dark" = "light") {
  applyCssVariables(mode);

  const isDark = mode === "dark";

  const baseTheme = createTheme({
    palette: {
      mode,

      primary: {
        main: isDark
          ? tokens.colors.accent.dark
          : tokens.colors.accent.light,
      },

      success: {
        main: isDark
          ? tokens.colors.success.dark
          : tokens.colors.success.light,
      },

      error: {
        main: isDark
          ? tokens.colors.error.dark
          : tokens.colors.error.light,
      },

      warning: {
        main: isDark
          ? tokens.colors.warning.dark
          : tokens.colors.warning.light,
      },

      info: {
        main: isDark
          ? tokens.colors.info.dark
          : tokens.colors.info.light,
      },

      background: {
        default: isDark
          ? tokens.colors.background.dark
          : tokens.colors.background.light,

        paper: isDark
          ? tokens.colors.surface.dark
          : tokens.colors.surface.light,
      },

      text: {
        primary: isDark
          ? tokens.colors.text.primaryDark
          : tokens.colors.text.primaryLight,

        secondary: isDark
          ? tokens.colors.text.secondaryDark
          : tokens.colors.text.secondaryLight,
      },

      divider: isDark
        ? tokens.colors.border.dark
        : tokens.colors.border.light,
    },

    typography: {
      fontFamily: tokens.typography.fontFamily,
    },

    shape: {
      borderRadius: 10,
    },

    components: {
      MuiButton: {
        defaultProps: {
          variant: "contained",
        },
        styleOverrides: {
          root: {
            textTransform: "none",
            borderRadius: 8,
            fontWeight: 500,
          },
        },
      },

      MuiTextField: {
        defaultProps: {
          variant: "outlined",
          fullWidth: true,
        },
      },

      MuiPaper: {
        styleOverrides: {
          root: {
            backgroundImage: "none",
          },
        },
      },

      MuiCard: {
        styleOverrides: {
          root: {
            border: "1px solid var(--border)",
            boxShadow: "none",
          },
        },
      },

      MuiAlert: {
        styleOverrides: {
          root: {
            borderRadius: 8,
          },
        },
      },

      MuiSnackbar: {
        defaultProps: {
          anchorOrigin: {
            vertical: "top",
            horizontal: "right",
          },
        },
      },
    },
  });

  return createTheme(baseTheme, ptBR);
}

export const theme = createAppTheme("light");