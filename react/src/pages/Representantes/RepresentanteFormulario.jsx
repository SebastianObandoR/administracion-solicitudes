
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Swal from "sweetalert2";
import ActionButton from "../../components/ui/ActionButton";
import ModuleHeader from "../../components/ui/ModuleHeader";
import ModulePanel from "../../components/ui/ModulePanel";
import {
  getRepresentanteById,
  createRepresentante,
  updateRepresentante
} from "../../services/representanteService";
import { logout } from "../../services/authService";

function RepresentanteFormulario() {
  const navigate = useNavigate();
  const { id } = useParams();
  const editar = Boolean(id);

  const [nombre, setNombre] = useState("");
  const [correo, setCorreo] = useState("");
  const [cargando, setCargando] = useState(editar);
  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    if (!editar) return;

    let activo = true;

    const cargarRepresentante = async () => {
      try {
        const representante = await getRepresentanteById(id);

        if (!activo) return;

        setNombre(representante.nombre);
        setCorreo(representante.correo);
      } catch (err) {
        if (!activo) return;

        if (err.response?.status === 401) {
          logout();
          navigate("/login", {
            replace: true,
            state: { motivo: "sinSesion" }
          });
          return;
        }

        await Swal.fire({
          icon: "error",
          title: "Error",
          text: "No se pudo cargar el representante.",
          confirmButtonColor: "#287d7b"
        });

        navigate("/representantes");
      } finally {
        if (activo) {
          setCargando(false);
        }
      }
    };

    cargarRepresentante();

    return () => {
      activo = false;
    };
  }, [id, editar, navigate]);

  const guardarRepresentante = async (event) => {
    event.preventDefault();

    if (guardando) return;

    const nombreLimpio = nombre.trim();
    const correoLimpio = correo.trim();

    if (!nombreLimpio || nombreLimpio.length > 150) {
      await Swal.fire({
        icon: "warning",
        title: "Validacion",
        text: "El nombre es requerido y no puede superar los 150 caracteres.",
        confirmButtonColor: "#287d7b"
      });
      return;
    }

    if (!correoLimpio || correoLimpio.length > 150) {
      await Swal.fire({
        icon: "warning",
        title: "Validacion",
        text: "El correo es requerido y no puede superar los 150 caracteres.",
        confirmButtonColor: "#287d7b"
      });
      return;
    }

    const correoValido = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(
      correoLimpio
    );

    if (!correoValido) {
      await Swal.fire({
        icon: "warning",
        title: "Validacion",
        text: "Ingrese un correo electronico valido.",
        confirmButtonColor: "#287d7b"
      });
      return;
    }

    setGuardando(true);

    try {
      if (editar) {
        await updateRepresentante(
          id,
          nombreLimpio,
          correoLimpio
        );
      } else {
        await createRepresentante(
          nombreLimpio,
          correoLimpio
        );
      }

      await Swal.fire({
        icon: "success",
        title: editar
          ? "Representante actualizado"
          : "Representante creado",
        text: editar
          ? "El representante se actualizo correctamente."
          : "El representante se registro correctamente.",
        confirmButtonColor: "#287d7b"
      });

      navigate("/representantes");
    } catch (err) {
      if (err.response?.status === 401) {
        logout();
        navigate("/login", {
          replace: true,
          state: { motivo: "sinSesion" }
        });
        return;
      }

      const mensaje =
        err.response?.data?.mensaje ??
        "No se pudo guardar el representante.";

      await Swal.fire({
        icon: "error",
        title: "Error",
        text: mensaje,
        confirmButtonColor: "#287d7b"
      });
    } finally {
      setGuardando(false);
    }
  };

  if (cargando) {
    return (
      <div className="crud-page crud-loading">
        <div className="spinner-border text-success"></div>
        <span>Cargando representante...</span>
      </div>
    );
  }

  return (
    <form
      className="crud-page"
      onSubmit={guardarRepresentante}
    >
      <ModuleHeader
        title={editar ? "Editar representante" : "Nuevo representante"}
      >
        <ActionButton
          icon="bi-arrow-left"
          round
          title="Volver"
          onClick={() => navigate("/representantes")}
        />

        <ActionButton
          icon="bi-floppy"
          round
          title="Guardar"
          type="submit"
          disabled={guardando}
        />
      </ModuleHeader>

      <ModulePanel title="Formulario Representante">
        <div className="mb-3">
          <label
            htmlFor="nombreRepresentante"
            className="form-label crud-label"
          >
            Nombre
          </label>

          <input
            type="text"
            id="nombreRepresentante"
            className="form-control crud-input"
            value={nombre}
            onChange={(event) => setNombre(event.target.value)}
            maxLength={150}
            autoComplete="off"
            required
            disabled={guardando}
          />
        </div>

        <div className="mb-3">
          <label
            htmlFor="correoRepresentante"
            className="form-label crud-label"
          >
            Correo electronico
          </label>

          <input
            type="email"
            id="correoRepresentante"
            className="form-control crud-input"
            value={correo}
            onChange={(event) => setCorreo(event.target.value)}
            maxLength={150}
            autoComplete="off"
            required
            disabled={guardando}
          />
        </div>

        {!editar && (
          <div className="crud-help-text">
            El identificador se asignara automaticamente.
          </div>
        )}
      </ModulePanel>
    </form>
  );
}

export default RepresentanteFormulario;
