import { api } from "../../../api/axios";
import type { LoginRequest } from "../types/loginRequest";
import type { LoginResponse } from "../types/loginResponse";

export async function login(data: LoginRequest) {
  const response = await api.post<LoginResponse>(
    "/auth",
    data
  );

  return response.data;
}