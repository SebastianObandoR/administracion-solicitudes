import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import Swal from "sweetalert2";
import {
  guardarRepresentante,
  mensajesDeError,
  obtenerRepresentante,
} from "../../services/representanteService";

function mostrarErroresFormulario(mensajes, icono = "error", titulo) {
  const lista = document.createElement("ul");
  lista.className = "representantes-errores";

  mensajes.forEach((mensaje) => {
    const elemento = document.createElement("li");
    elemento.textContent = mensaje;
    lista.appendChild(elemento);
  });

  return Swal.fire({
    title: titulo,
    html: lista,
    icon: icono,
    confirmButtonText: "Aceptar",
    confirmButtonColor: "#2d7a78",
  });
}

function Formulario({ id }) {
  const navigate = useNavigate();
  const [input, setInput] = useState(id ? null : { nombre: "", correo: "" });
  const [errorCarga, setErrorCarga] = useState("");
  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    if (!id) return;

    const controller = new AbortController();
    obtenerRepresentante(id, controller.signal)
      .then(({ nombre, correo }) => {
        if (!controller.signal.aborted) setInput({ nombre, correo });
      })
      .catch((err) => {
        if (!controller.signal.aborted) {
          setErrorCarga(mensajesDeError(err, "No se pudo cargar el representante.")[0]);
        }
      });

    return () => controller.abort();
  }, [id]);

  async function guardar(event) {
    event.preventDefault();
    if (event.currentTarget.elements.correo.validity.typeMismatch) {
      await mostrarErroresFormulario(["Ingrese un email válido."], "warning", "Campos obligatorios");
      return;
    }

    setGuardando(true);
    try {
      const { mensaje, representante } = await guardarRepresentante(input, id);
      if (!id) {
        navigate("/representantes", {
          replace: true,
          state: { alta: { idRepresentante: representante.idRepresentante, mensaje } },
        });
        return;
      }

      await Swal.fire({ icon: "success", text: mensaje, confirmButtonText: "Aceptar" });
      navigate("/representantes", { replace: true });
    } catch (err) {
      await mostrarErroresFormulario(mensajesDeError(err, "No se pudo guardar el representante."));
    } finally {
      setGuardando(false);
    }
  }

  return (
    <section className="formulario-mantenimiento" aria-labelledby="titulo-formulario-representante">
      <div className="acciones-formulario">
        <Link className="boton-formulario" to="/representantes" title="Regresar" aria-label="Regresar">
          <i className="bi bi-arrow-left" aria-hidden="true" />
        </Link>
        <button
          type="submit"
          form="form-representante"
          className="boton-formulario"
          title="Guardar"
          aria-label="Guardar"
          disabled={!input || guardando}
        >
          <i className="bi bi-floppy-fill" aria-hidden="true" />
        </button>
      </div>

      <div className="panel-formulario">
        <h1 className="titulo-panel" id="titulo-formulario-representante">Formulario Representante</h1>

        {!input && (
          <div className="campos-formulario">
            {errorCarga ? <p role="alert">{errorCarga}</p> : <p role="status">Cargando representante…</p>}
          </div>
        )}

        {input && (
          <form id="form-representante" className="campos-formulario" onSubmit={guardar} aria-busy={guardando} noValidate>
            <div className="campo-formulario">
              <label htmlFor="nombre-representante">Nombre</label>
              <input
                id="nombre-representante"
                name="nombre"
                autoComplete="name"
                value={input.nombre}
                onChange={(event) => setInput({ ...input, nombre: event.target.value })}
                required
              />
            </div>
            <div className="campo-formulario">
              <label htmlFor="correo-representante">Email</label>
              <input
                id="correo-representante"
                name="correo"
                type="email"
                inputMode="email"
                autoComplete="email"
                value={input.correo}
                onChange={(event) => setInput({ ...input, correo: event.target.value })}
                required
              />
            </div>
          </form>
        )}
      </div>
    </section>
  );
}

function RepresentanteForm() {
  const { id } = useParams();
  return <Formulario key={id ?? "nuevo"} id={id} />;
}

export default RepresentanteForm;
