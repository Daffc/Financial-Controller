import { api } from "./axios";


interface ApiErrorResponse {
  message?: string;
  errors?: string[];
  traceId?: string;
}

export function extractApiError(error: any): string {
  const data: ApiErrorResponse = error?.response?.data;

  if(data){
    let msg = data.message || "Erro desconhecido";

    if (data.errors && data.errors.length > 0) {
      msg += ":\n" + data.errors.map(e => `  • ${e}`).join("\n");
    }
  
    if (data.errors && data.errors.length > 0) {
      msg += `\nTraceId: ${data.traceId}`;
    }

    return msg;
  }
  
  if (error?.message === "Network Error" || error?.code === "ERR_NETWORK") {
    return "Erro de conexão com o servidor.";
  }

  return "Erro desconhecido"
}

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

api.interceptors.response.use(
  (res) => res,
  (err) => {
    if (err.response?.status === 401) {
      localStorage.removeItem("token");
      window.location.href = "/login";
    }
    return Promise.reject(err);
  }
);