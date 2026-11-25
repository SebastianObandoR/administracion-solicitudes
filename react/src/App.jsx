
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate
} from "react-router-dom";

import Login from "./pages/login/Login";
import Bienvenida from "./pages/Bienvenida/Bienvenida";
import Estados from "./pages/Estados/Estados";
import EstadoFormulario from "./pages/Estados/EstadoFormulario";
import Representantes from "./pages/Representantes/Representantes";
import RepresentanteFormulario from "./pages/Representantes/RepresentanteFormulario";
import ProtectedRoute from "./routes/ProtectedRoute";
import AppLayout from "./components/AppLayout";
import { getAccessToken } from "./services/authService";

import "./styles/crud.css";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <Navigate
              to={getAccessToken() ? "/bienvenida" : "/login"}
              replace
            />
          }
        />

        <Route path="/login" element={<Login />} />

        <Route element={<ProtectedRoute />}>
          <Route element={<AppLayout />}>
            <Route
              path="/bienvenida"
              element={<Bienvenida />}
            />

            <Route
              path="/estados"
              element={<Estados />}
            />

            <Route
              path="/estados/nuevo"
              element={<EstadoFormulario />}
            />

            <Route
              path="/estados/editar/:id"
              element={<EstadoFormulario />}
            />

            <Route
              path="/representantes"
              element={<Representantes />}
            />

            <Route
              path="/representantes/nuevo"
              element={<RepresentanteFormulario />}
            />

            <Route
              path="/representantes/editar/:id"
              element={<RepresentanteFormulario />}
            />
          </Route>
        </Route>

        <Route
          path="*"
          element={<Navigate to="/" replace />}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
