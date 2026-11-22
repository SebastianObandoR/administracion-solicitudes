
import { NavLink } from "react-router-dom";
import BrandLogo from "./BrandLogo";

const modulos = [
  { nombre: "Solicitudes", icono: "bi-file-earmark-text" },
  { nombre: "Usuarios", icono: "bi-people" },
  { nombre: "Representantes", icono: "bi-person-vcard" },
  { nombre: "Estados", icono: "bi-diagram-3" },
  { nombre: "Facturacion mensual", icono: "bi-bar-chart" },
  { nombre: "Tareas por usuario", icono: "bi-person-lines-fill" },
  { nombre: "Tareas por solicitud", icono: "bi-card-checklist" },
];

function Sidebar({ abierto, cerrar }) {
  return (
    <aside className={`app-sidebar ${abierto ? "is-open" : ""}`}>
      <div className="sidebar-brand">
        <BrandLogo size="sm" />

        <div className="brand-information">
          <h2>PUBLICA S.A.</h2>
          <p>Administracion de Solicitudes</p>
        </div>
      </div>

      <nav className="sidebar-navigation">
        <span className="sidebar-section-title">
          MENU PRINCIPAL
        </span>

        <NavLink
          to="/bienvenida"
          onClick={cerrar}
          className={({ isActive }) =>
            `sidebar-link ${isActive ? "active" : ""}`
          }
        >
          <i className="bi bi-speedometer2"></i>
          <span>Inicio</span>
        </NavLink>

        {modulos.map((modulo) => (
          <div
            key={modulo.nombre}
            className="sidebar-link sidebar-link-disabled"
            title="Modulo pendiente"
          >
            <i className={`bi ${modulo.icono}`}></i>
            <span>{modulo.nombre}</span>
          </div>
        ))}
      </nav>

      <div className="sidebar-footer">
        Administracion de Solicitudes
      </div>
    </aside>
  );
}

export default Sidebar;
