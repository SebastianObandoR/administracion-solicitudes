import { NavLink, useMatch } from "react-router-dom";
import "./representantes.css";

const modulos = [
  { nombre: "Inicio", icono: "bi-speedometer2", ruta: "/bienvenida" },
  { nombre: "Solicitudes", icono: "bi-file-earmark-text" },
  { nombre: "Usuarios", icono: "bi-people" },
  { nombre: "Representantes", icono: "bi-person-badge", ruta: "/representantes" },
  { nombre: "Estados", icono: "bi-diagram-3" },
  { nombre: "Facturación mensual", icono: "bi-bar-chart-line" },
  { nombre: "Tareas por usuario", icono: "bi-person-lines-fill" },
  { nombre: "Tareas por solicitud", icono: "bi-card-checklist" },
];

function RepresentantesLayout({ usuario, abierto, onToggleMenu, onCloseMenu, onLogout, children }) {
  const alta = useMatch("/representantes/nuevo");
  const edicion = useMatch("/representantes/:id/editar");
  const titulo = alta ? "Nuevo representante" : edicion ? "Editar representante" : "Representantes";

  return (
    <div className="representantes-layout">
      <aside className={`panel-lateral ${abierto ? "esta-abierto" : ""}`} id="navegacion-sol3">
        <div className="marca-sistema">
          <div className="logo-sistema-sidebar">
            <i className="bi bi-ui-checks-grid" aria-hidden="true" />
          </div>
          <div className="identidad-sistema">
            <span className="nombre-empresa">PUBLICA S.A.</span>
            <span className="nombre-sistema">Administracion de Solicitudes</span>
          </div>
        </div>

        <nav className="navegacion-principal" aria-label="Navegación principal">
          {modulos.map(({ nombre, icono, ruta }) => {
            const contenido = (
              <>
                <i className={`bi ${icono} icono-navegacion`} aria-hidden="true" />
                <span className="texto-navegacion">{nombre}</span>
              </>
            );

            return ruta ? (
              <NavLink
                key={nombre}
                to={ruta}
                onClick={onCloseMenu}
                className={({ isActive }) => `elemento-navegacion ${isActive ? "esta-activo" : ""}`}
              >
                {contenido}
              </NavLink>
            ) : (
              <span key={nombre} className="elemento-navegacion" aria-disabled="true">
                {contenido}
              </span>
            );
          })}
        </nav>

        <div className="pie-lateral"><span>ASP.NET 10</span><span>v1.0</span></div>
      </aside>

      {abierto && (
        <button type="button" className="fondo-navegacion" onClick={onCloseMenu} aria-label="Cerrar menú" />
      )}

      <div className="contenido-trabajo">
        <header className="panel-superior">
          <button
            className="boton-navegacion"
            type="button"
            aria-label="Abrir menú"
            aria-expanded={abierto}
            aria-controls="navegacion-sol3"
            onClick={onToggleMenu}
          >
            <i className="bi bi-list" aria-hidden="true" />
          </button>
          <div className="contexto-pagina">
            <span className="etiqueta-contexto">Area de trabajo</span>
            <span className="titulo-contexto">{titulo}</span>
          </div>

          <div className="area-perfil">
            <div className="informacion-perfil">
              <span className="etiqueta-perfil">Sesion actual</span>
              <span className="nombre-perfil">{usuario?.nombreCompleto}</span>
            </div>
            <details className="menu-perfil">
              <summary className="marca-perfil" aria-label="Opciones de sesión" title={usuario?.nombreCompleto}>
                <i className="bi bi-person icono-usuario" aria-hidden="true" />
              </summary>
              <button type="button" className="salir-perfil" onClick={onLogout}>
                Cerrar sesión
              </button>
            </details>
          </div>
        </header>

        <main className="contenido-pagina">{children}</main>
      </div>
    </div>
  );
}

export default RepresentantesLayout;
