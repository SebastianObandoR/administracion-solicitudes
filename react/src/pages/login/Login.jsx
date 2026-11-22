
import { useEffect, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import { login } from "../../services/authService";
import BrandLogo from "../../components/BrandLogo";

function Login() {
  const navigate = useNavigate();
  const location = useLocation();
  const alertaMostrada = useRef(false);

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const mensajeCredenciales = "Usuario y/o contrase\u00f1a incorrectos.";

  useEffect(() => {
    if (
      location.state?.motivo !== "sinSesion" ||
      alertaMostrada.current
    ) {
      return;
    }

    alertaMostrada.current = true;

    navigate("/login", { replace: true, state: null });

    Swal.fire({
      icon: "warning",
      title: "Sesion requerida",
      text: "Por favor inicie sesi\u00f3n para utilizar el sistema",
      confirmButtonText: "Aceptar",
      confirmButtonColor: "#28666c",
    });
  }, [location.state?.motivo, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!username.trim() || !password.trim()) {
      await Swal.fire({
        icon: "warning",
        title: "Atencion",
        text: mensajeCredenciales,
        confirmButtonText: "Aceptar",
        confirmButtonColor: "#28666c",
      });
      return;
    }

    setLoading(true);

    try {
      await login(username.trim(), password);

      await Swal.fire({
        icon: "success",
        title: "Ingreso correcto",
        text: "Bienvenido al sistema.",
        timer: 1200,
        showConfirmButton: false,
      });

      navigate("/bienvenida", { replace: true });
    } catch (err) {
      const credencialesInvalidas =
        err.response?.status === 401;

      await Swal.fire({
        icon: "error",
        title: credencialesInvalidas
          ? "Acceso denegado"
          : "Error de conexion",
        text: credencialesInvalidas
          ? mensajeCredenciales
          : "No fue posible conectar con el servidor.",
        confirmButtonText: "Aceptar",
        confirmButtonColor: "#28666c",
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <main
      className="min-vh-100 d-flex align-items-center justify-content-center px-3"
      style={{
        background: "linear-gradient(135deg, #e9f7f3, #f5f8f8)",
      }}
    >
      <div
        className="card border-0 shadow p-4"
        style={{
          width: "100%",
          maxWidth: "430px",
          borderRadius: "20px",
        }}
      >
        <div className="card-body">
          <div className="text-center mb-4">
            <div className="d-flex justify-content-center mb-3">
              <BrandLogo size="md" />
            </div>

            <h3
              className="fw-bold mt-3"
              style={{ color: "#193f45" }}
            >
              Administracion de Solicitudes
            </h3>

            <p className="text-muted mt-2">
              Inicie sesion para continuar
            </p>
          </div>

          <form onSubmit={handleSubmit} noValidate>
            <div className="mb-3">
              <label
                htmlFor="username"
                className="form-label fw-semibold"
              >
                Usuario
              </label>

              <input
                id="username"
                type="text"
                className="form-control py-2"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="Ingrese su usuario"
                autoComplete="username"
                disabled={loading}
              />
            </div>

            <div className="mb-4">
              <label
                htmlFor="password"
                className="form-label fw-semibold"
              >
                Contrasena
              </label>

              <input
                id="password"
                type="password"
                className="form-control py-2"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Ingrese su contrasena"
                autoComplete="current-password"
                disabled={loading}
              />
            </div>

            <button
              type="submit"
              className="btn w-100 py-2 fw-semibold text-white"
              disabled={loading}
              style={{
                backgroundColor: "#28666c",
                borderRadius: "10px",
              }}
            >
              {loading ? (
                <>
                  <span className="spinner-border spinner-border-sm me-2" />
                  Validando...
                </>
              ) : (
                <>
                  <i className="bi bi-box-arrow-in-right me-2"></i>
                  Ingresar
                </>
              )}
            </button>
          </form>
        </div>
      </div>
    </main>
  );
}

export default Login;
