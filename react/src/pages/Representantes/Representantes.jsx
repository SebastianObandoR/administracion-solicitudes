import { useEffect, useRef, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import {
  eliminarRepresentante,
  listarRepresentantes,
  mensajesDeError,
} from "../../services/representanteService";

function Representantes() {
  const { state } = useLocation();
  const navigate = useNavigate();
  const [consulta, setConsulta] = useState(() => ({ pagina: 1, alta: state?.alta }));
  const [resultado, setResultado] = useState(null);
  const [error, setError] = useState("");
  const [eliminando, setEliminando] = useState(false);
  const altaMostrada = useRef(false);

  useEffect(() => {
    if (state?.alta) {
      navigate("/representantes", { replace: true });
    }
  }, [navigate, state]);

  useEffect(() => {
    const controller = new AbortController();
    listarRepresentantes(consulta.pagina, controller.signal, consulta.alta?.idRepresentante)
      .then((data) => {
        if (controller.signal.aborted) return;

        const ultimaPagina = Math.max(1, data.totalPaginas);
        if (data.pagina > ultimaPagina) {
          setConsulta({ pagina: ultimaPagina, alta: consulta.alta });
        } else {
          setResultado(data);
        }
      })
      .catch((err) => {
        if (!controller.signal.aborted) {
          setError(mensajesDeError(err, "No se pudieron cargar los representantes.")[0]);
        }
      });

    return () => controller.abort();
  }, [consulta]);

  useEffect(() => {
    if (resultado && consulta.alta && !altaMostrada.current) {
      altaMostrada.current = true;
      Swal.fire({ icon: "success", text: consulta.alta.mensaje, confirmButtonText: "Aceptar" });
    }
  }, [consulta.alta, resultado]);

  function cargarPagina(pagina, alta) {
    setResultado(null);
    setError("");
    setConsulta({ pagina, alta });
  }

  async function confirmarEliminacion(id) {
    setEliminando(true);
    try {
      const confirmacion = await Swal.fire({
        title: "Confirmar eliminación",
        text: "¿Realmente desea eliminar el elemento seleccionado?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí",
        cancelButtonText: "No",
        focusCancel: true,
      });
      if (!confirmacion.isConfirmed) return;

      const { mensaje } = await eliminarRepresentante(id);
      cargarPagina(resultado.pagina);
      await Swal.fire({ icon: "success", text: mensaje, confirmButtonText: "Aceptar" });
    } catch (err) {
      const [mensaje] = mensajesDeError(err, "No se pudo eliminar el representante.");
      await Swal.fire({ icon: "error", text: mensaje, confirmButtonText: "Aceptar" });
    } finally {
      setEliminando(false);
    }
  }

  return (
    <section className="mantenimiento" aria-labelledby="titulo-representantes">
      <div className="cabecera-mantenimiento">
        <h1 id="titulo-representantes">Administracion de Representantes</h1>
        <Link className="boton-nuevo" to="/representantes/nuevo" aria-label="Nuevo representante">
          <i className="bi bi-file-earmark-plus-fill" aria-hidden="true" />
          <span>Nuevo</span>
        </Link>
      </div>

      <div className="panel-informacion">
        <div className="titulo-panel">Informacion</div>
        <div className="contenedor-tabla">
          {error && (
            <div role="alert">
              <p className="mb-2">{error}</p>
              <button type="button" className="boton-nuevo" onClick={() => cargarPagina(consulta.pagina, consulta.alta)}>
                Reintentar
              </button>
            </div>
          )}
          {!resultado && !error && <p role="status">Cargando representantes…</p>}

          {resultado && (
            <table className="tabla-mantenimiento">
              <thead>
                <tr>
                  <th scope="col">Nombre</th>
                  <th scope="col">Email</th>
                  <th scope="col" className="columna-acciones">Acciones</th>
                </tr>
              </thead>
              <tbody>
                {resultado.representantes.length === 0 && (
                  <tr><td colSpan={3} className="sin-registros">No hay representantes para mostrar.</td></tr>
                )}
                {resultado.representantes.map(({ idRepresentante, nombre, correo }) => (
                  <tr key={idRepresentante}>
                    <td>{nombre}</td>
                    <td>{correo}</td>
                    <td className="acciones">
                      <Link
                        className="boton-accion"
                        to={`/representantes/${idRepresentante}/editar`}
                        aria-label={`Editar representante ${nombre}`}
                        title="Editar"
                      >
                        <i className="bi bi-pencil-square" aria-hidden="true" />
                      </Link>{" "}
                      <button
                        type="button"
                        className="boton-accion"
                        onClick={() => confirmarEliminacion(idRepresentante)}
                        disabled={eliminando}
                        aria-label={`Eliminar representante ${nombre}`}
                        title="Eliminar"
                      >
                        <i className="bi bi-trash-fill" aria-hidden="true" />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>

        {resultado?.totalPaginas > 1 && (
          <nav className="paginacion" aria-label="Paginación de representantes">
            {Array.from({ length: resultado.totalPaginas }, (_, indice) => {
              const pagina = indice + 1;
              const actual = pagina === resultado.pagina;
              return (
                <button
                  key={pagina}
                  type="button"
                  className={`pagina ${actual ? "pagina-activa" : ""}`}
                  onClick={() => cargarPagina(pagina)}
                  aria-current={actual ? "page" : undefined}
                  disabled={eliminando}
                >
                  {pagina}
                </button>
              );
            })}
          </nav>
        )}
      </div>
    </section>
  );
}

export default Representantes;
