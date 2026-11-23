import axios from "axios";
import { getAccessToken, logout } from "./authService";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

api.interceptors.request.use((config) => {
  const token = getAccessToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response.data,
  (error) => {
    if (error.response?.status === 401) {
      logout();
      window.location.replace("/login");
    }
    return Promise.reject(error);
  },
);

const ruta = "/api/representantes";

export const listarRepresentantes = (pagina, signal, idRepresentante) =>
  api.get(ruta, { params: { pagina, idRepresentante }, signal });

export const obtenerRepresentante = (id, signal) => api.get(`${ruta}/${id}`, { signal });

export const guardarRepresentante = (input, id) =>
  id ? api.put(`${ruta}/${id}`, input) : api.post(ruta, input);

export const eliminarRepresentante = (id) => api.delete(`${ruta}/${id}`);

export function mensajesDeError(error, mensajePredeterminado) {
  const data = error.response?.data;
  if (data?.errors) {
    return Object.values(data.errors).flat();
  }
  return [data?.mensaje ?? mensajePredeterminado];
}
