
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";

import Login from "./pages/login/Login";
import Bienvenida from "./pages/Bienvenida/Bienvenida";
import Representantes from "./pages/Representantes/Representantes";
import RepresentanteForm from "./pages/Representantes/RepresentanteForm";
import ProtectedRoute from "./routes/ProtectedRoute";
import AppLayout from "./components/AppLayout";
import { getAccessToken } from "./services/authService";

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
            <Route path="/representantes" element={<Representantes />} />
            <Route path="/representantes/nuevo" element={<RepresentanteForm />} />
            <Route path="/representantes/:id/editar" element={<RepresentanteForm />} />
          </Route>
        </Route>

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
