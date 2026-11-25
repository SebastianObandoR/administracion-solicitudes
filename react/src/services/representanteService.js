
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

export const getRepresentantes = async (
  pageNumber = 1,
  pageSize = 10
) => {
  const response = await api.get("/api/representantes/page", {
    params: {
      pageNumber,
      pageSize
    }
  });

  return response.data;
};

export const getRepresentanteById = async (id) => {
  const response = await api.get(`/api/representantes/${id}`);
  return response.data;
};

export const createRepresentante = async (nombre, correo) => {
  const response = await api.post("/api/representantes", {
    nombre,
    correo
  });

  return response.data;
};

export const updateRepresentante = async (id, nombre, correo) => {
  const response = await api.put(`/api/representantes/${id}`, {
    nombre,
    correo
  });

  return response.data;
};

export const deleteRepresentante = async (id) => {
  await api.delete(`/api/representantes/${id}`);
};
