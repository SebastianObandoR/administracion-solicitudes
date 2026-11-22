
import { useOutletContext } from "react-router-dom";
import BrandLogo from "../../components/BrandLogo";

function Bienvenida() {
  const { usuario } = useOutletContext();

  return (
    <section className="welcome-page">
      <div className="welcome-content">
        <div className="welcome-text">
          <h1>
            Bienvenido, {usuario?.nombreCompleto}
          </h1>
        </div>

        <div className="welcome-logo">
          <BrandLogo size="xl" />
        </div>
      </div>
    </section>
  );
}

export default Bienvenida;
