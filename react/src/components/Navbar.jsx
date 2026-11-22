
function Navbar({ usuario, onLogout, onToggleMenu }) {
  return (
    <header className="app-navbar">
      <div className="navbar-location">
        <button
          type="button"
          className="navbar-menu-button"
          onClick={onToggleMenu}
          aria-label="Abrir menu"
        >
          <i className="bi bi-list"></i>
        </button>

        <div>
          <span className="navbar-label">
            AREA DE TRABAJO
          </span>

          <h3>Inicio</h3>
        </div>
      </div>

      <div className="navbar-user">
        <div className="navbar-user-info">
          <span>Sesion actual</span>
          <strong>{usuario?.nombreCompleto}</strong>
        </div>

        <div className="navbar-avatar">
          <i className="bi bi-person"></i>
        </div>

        <button
          type="button"
          className="navbar-logout"
          onClick={onLogout}
          title="Cerrar sesion"
          aria-label="Cerrar sesion"
        >
          <i className="bi bi-box-arrow-right"></i>
        </button>
      </div>
    </header>
  );
}

export default Navbar;
