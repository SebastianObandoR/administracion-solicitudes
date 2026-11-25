
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Swal from "sweetalert2";
import ActionButton from "../../components/ui/ActionButton";
import ModuleHeader from "../../components/ui/ModuleHeader";
import ModulePanel from "../../components/ui/ModulePanel";
import {
  getEstadoById,
  createEstado,
  updateEstado
} from "../../services/estadoService";
import { logout } from "../../services/authService";

function EstadoFormulario() {
  const navigate = useNavigate();
  const { id } = useParams();
  const editar = Boolean(id);

  const [nombre, setNombre] = useState("");
  const [cargando, setCargando] = useState(editar);
  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    if (!editar) return;

    let activo = true;

    const cargarEstado = async () => {
      try {
        const estado = await getEstadoById(id);

        if (activo) {
          setNombre(estado.nombre);
        }
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
          text: "No se pudo cargar el estado.",
          confirmButtonColor: "#287d7b"
        });

        navigate("/estados");
      } finally {
        if (activo) {
          setCargando(false);
        }
      }
    };

    cargarEstado();

    return () => {
      activo = false;
    };
  }, [id, editar, navigate]);

  const guardarEstado = async (event) => {
    event.preventDefault();

    if (guardando) return;

    const nombreLimpio = nombre.trim();

    if (!nombreLimpio || nombreLimpio.length > 50) {
      await Swal.fire({
        icon: "warning",
        title: "Validacion",
        text: "El nombre es requerido y no puede superar los 50 caracteres.",
        confirmButtonColor: "#287d7b"
      });
      return;
    }

    setGuardando(true);

    try {
      if (editar) {
        await updateEstado(id, nombreLimpio);
      } else {
        await createEstado(nombreLimpio);
      }

      await Swal.fire({
        icon: "success",
        title: editar ? "Estado actualizado" : "Estado creado",
        text: editar
          ? "El estado se actualizo correctamente."
          : "El estado se registro correctamente.",
        confirmButtonColor: "#287d7b"
      });

      navigate("/estados");
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
        "No se pudo guardar el estado.";

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
        <span>Cargando estado...</span>
      </div>
    );
  }

  return (
    <form className="crud-page" onSubmit={guardarEstado}>
      <ModuleHeader title={editar ? "Editar estado" : "Nuevo estado"}>
        <ActionButton
          icon="bi-arrow-left"
          round
          title="Volver"
          onClick={() => navigate("/estados")}
        />

        <ActionButton
          icon="bi-floppy"
          round
          title="Guardar"
          type="submit"
          disabled={guardando}
        />
      </ModuleHeader>

      <ModulePanel title="Formulario Estado de Solicitud">
        <div className="mb-3">
          <label htmlFor="nombreEstado" className="form-label crud-label">
            Nombre
          </label>

          <input
            type="text"
            id="nombreEstado"
            className="form-control crud-input"
            value={nombre}
            onChange={(event) => setNombre(event.target.value)}
            maxLength={50}
            autoComplete="off"
            required
            disabled={guardando}
          />

          {!editar && (
            <div className="crud-help-text">
              El identificador se asignara automaticamente.
            </div>
          )}
        </div>
      </ModulePanel>
    </form>
  );
}

export default EstadoFormulario;
