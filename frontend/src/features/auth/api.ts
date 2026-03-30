import { api } from "../../api/axios";
import type { LoginRequest, LoginResponse } from "./types";

export async function login(data: LoginRequest) {
  const response = await api.post<LoginResponse>(
    "/auth",
    data
  );

  return response.data;
}