
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import ActionButton from "../../components/ui/ActionButton";
import DataTable from "../../components/ui/DataTable";
import TablePagination from "../../components/ui/TablePagination";
import ModuleHeader from "../../components/ui/ModuleHeader";
import ModulePanel from "../../components/ui/ModulePanel";
import {
  getRepresentantes,
  deleteRepresentante
} from "../../services/representanteService";
import { logout } from "../../services/authService";

function Representantes() {
  const navigate = useNavigate();

  const [representantes, setRepresentantes] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [revision, setRevision] = useState(0);

  useEffect(() => {
    let activo = true;

    const cargarRepresentantes = async () => {
      setLoading(true);

      try {
        const resultado = await getRepresentantes(
          pageNumber,
          pageSize
        );

        if (!activo) return;

        setRepresentantes(resultado.items ?? []);
        setTotalCount(resultado.totalCount ?? 0);
        setTotalPages(resultado.totalPages ?? 0);
        setError("");

        const ultimaPagina = Math.max(
          1,
          resultado.totalPages ?? 0
        );

        if (pageNumber > ultimaPagina) {
          setPageNumber(ultimaPagina);
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

        setError("No se pudieron cargar los representantes.");
        setRepresentantes([]);
      } finally {
        if (activo) {
          setLoading(false);
        }
      }
    };

    cargarRepresentantes();

    return () => {
      activo = false;
    };
  }, [pageNumber, pageSize, revision, navigate]);

  const cambiarPagina = (pagina) => {
    setPageNumber(pagina);
  };

  const cambiarTamanoPagina = (tamano) => {
    setPageSize(tamano);
    setPageNumber(1);
  };

  const eliminarRepresentante = async (representante) => {
    const confirmacion = await Swal.fire({
      title: "Eliminar representante",
      text: `Desea eliminar a ${representante.nombre}?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Eliminar",
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#287d7b",
      cancelButtonColor: "#6c757d"
    });

    if (!confirmacion.isConfirmed) return;

    try {
      await deleteRepresentante(representante.idRepresentante);

      await Swal.fire({
        icon: "success",
        title: "Representante eliminado",
        text: "El representante fue eliminado correctamente.",
        confirmButtonColor: "#287d7b"
      });

      setRevision((actual) => actual + 1);
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
        "No se pudo eliminar el representante.";

      await Swal.fire({
        icon: "error",
        title: "Error",
        text: mensaje,
        confirmButtonColor: "#287d7b"
      });
    }
  };

  const columns = [
    {
      key: "idRepresentante",
      header: "Identificador",
      width: "18%"
    },
    {
      key: "nombre",
      header: "Nombre",
      width: "28%"
    },
    {
      key: "correo",
      header: "Correo electronico",
      width: "34%"
    },
    {
      key: "acciones",
      header: "Acciones",
      width: "20%",
      render: (representante) => (
        <div className="crud-row-actions">
          <ActionButton
            icon="bi-pencil-square"
            round
            title="Editar"
            onClick={() =>
              navigate(
                `/representantes/editar/${representante.idRepresentante}`
              )
            }
          />

          <ActionButton
            icon="bi-trash"
            round
            title="Eliminar"
            onClick={() =>
              eliminarRepresentante(representante)
            }
          />
        </div>
      )
    }
  ];

  return (
    <div className="crud-page">
      <ModuleHeader title="Administracion de Representantes">
        <ActionButton
          icon="bi-file-earmark-plus"
          onClick={() => navigate("/representantes/nuevo")}
        >
          Nuevo
        </ActionButton>
      </ModuleHeader>

      <ModulePanel title="Informacion">
        {error && (
          <div className="alert alert-danger d-flex justify-content-between align-items-center gap-3">
            <span>{error}</span>

            <ActionButton
              onClick={() => setRevision((actual) => actual + 1)}
            >
              Reintentar
            </ActionButton>
          </div>
        )}

        <DataTable
          columns={columns}
          rows={error ? [] : representantes}
          keyField="idRepresentante"
          loading={loading}
          emptyMessage="No hay representantes registrados."
        />

        <TablePagination
          pageNumber={pageNumber}
          pageSize={pageSize}
          totalCount={totalCount}
          totalPages={totalPages}
          onPageChange={cambiarPagina}
          onPageSizeChange={cambiarTamanoPagina}
        />
      </ModulePanel>
    </div>
  );
}

export default Representantes;
