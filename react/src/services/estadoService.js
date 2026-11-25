
import axios from "axios";
import { getAccessToken } from "./authService";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});

api.interceptors.request.use((config) => {
  const token = getAccessToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export const getEstados = async (pageNumber = 1, pageSize = 10) => {
  const response = await api.get("/api/estados/page", {
    params: {
      pageNumber,
      pageSize
    }
  });

  return response.data;
};

export const getEstadoById = async (id) => {
  const response = await api.get(`/api/estados/${id}`);
  return response.data;
};

export const createEstado = async (nombre) => {
  const response = await api.post("/api/estados", {
    nombre
  });

  return response.data;
};

export const updateEstado = async (id, nombre) => {
  const response = await api.put(`/api/estados/${id}`, {
    nombre
  });

  return response.data;
};

export const deleteEstado = async (id) => {
  await api.delete(`/api/estados/${id}`);
};
