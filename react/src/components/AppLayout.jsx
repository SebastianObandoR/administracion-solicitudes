
import { useEffect, useState } from "react";
import { Outlet, useMatch, useNavigate } from "react-router-dom";
import Sidebar from "./Sidebar";
import Navbar from "./Navbar";
import RepresentantesLayout from "../pages/Representantes/RepresentantesLayout";
import {
  getCurrentUser,
  logout,
} from "../services/authService";

function AppLayout() {
  const navigate = useNavigate();
  const enRepresentantes = useMatch("/representantes/*");

  const [usuario, setUsuario] = useState(null);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState("");
  const [menuAbierto, setMenuAbierto] = useState(false);

  useEffect(() => {
    let activo = true;

    getCurrentUser()
      .then((data) => {
        if (activo) {
          setUsuario(data);
        }
      })
      .catch((err) => {
        if (!activo) return;

        if (err.response?.status === 401) {
          logout();
          navigate("/login", {
            replace: true,
            state: { motivo: "sinSesion" },
          });
        } else {
          setError("No se pudo cargar la informacion.");
        }
      })
      .finally(() => {
        if (activo) {
          setCargando(false);
        }
      });

    return () => {
      activo = false;
    };
  }, [navigate]);

  const cerrarSesion = () => {
    logout();
    navigate("/login", { replace: true });
  };

  if (cargando) {
    return (
      <div className="loading-screen">
        <div className="spinner-border text-success"></div>
        <p>Cargando informacion...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="loading-screen">
        <p>{error}</p>

        <button
          type="button"
          className="btn btn-outline-success"
          onClick={() => window.location.reload()}
        >
          Reintentar
        </button>
      </div>
    );
  }

  if (enRepresentantes) {
    return (
      <RepresentantesLayout
        usuario={usuario}
        abierto={menuAbierto}
        onToggleMenu={() => setMenuAbierto(!menuAbierto)}
        onCloseMenu={() => setMenuAbierto(false)}
        onLogout={cerrarSesion}
      >
        <Outlet context={{ usuario }} />
      </RepresentantesLayout>
    );
  }

  return (
    <div className="app-shell">
      <Sidebar
        abierto={menuAbierto}
        cerrar={() => setMenuAbierto(false)}
      />

      {menuAbierto && (
        <button
          type="button"
          className="sidebar-backdrop"
          onClick={() => setMenuAbierto(false)}
          aria-label="Cerrar menu"
        />
      )}

      <div className="app-content">
        <Navbar
          usuario={usuario}
          onLogout={cerrarSesion}
          onToggleMenu={() => setMenuAbierto(!menuAbierto)}
        />

        <main className="app-main">
          <Outlet context={{ usuario }} />
        </main>
      </div>
    </div>
  );
}

export default AppLayout;
