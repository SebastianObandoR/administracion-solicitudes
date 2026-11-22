
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export const login = async (username, password) => {
  const response = await api.post("/api/auth/login", {
    username,
    password,
  });

  sessionStorage.setItem("accessToken", response.data.accessToken);

  return response.data;
};

export const getAccessToken = () => {
  return sessionStorage.getItem("accessToken");
};

export const getCurrentUser = async () => {
  const token = getAccessToken();

  const response = await api.get("/api/auth/me", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  return response.data;
};

export const logout = () => {
  sessionStorage.removeItem("accessToken");
};
