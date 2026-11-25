
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import ActionButton from "../../components/ui/ActionButton";
import DataTable from "../../components/ui/DataTable";
import TablePagination from "../../components/ui/TablePagination";
import ModuleHeader from "../../components/ui/ModuleHeader";
import ModulePanel from "../../components/ui/ModulePanel";
import { getEstados, deleteEstado } from "../../services/estadoService";
import { logout } from "../../services/authService";

function Estados() {
  const navigate = useNavigate();

  const [estados, setEstados] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [revision, setRevision] = useState(0);

  useEffect(() => {
    let activo = true;

    const cargarEstados = async () => {
      try {
        const resultado = await getEstados(pageNumber, pageSize);

        if (!activo) return;

        setEstados(resultado.items ?? []);
        setTotalCount(resultado.totalCount ?? 0);
        setTotalPages(resultado.totalPages ?? 0);
        setError("");

        const ultimaPagina = Math.max(1, resultado.totalPages ?? 0);

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

        setError("No se pudieron cargar los estados.");
      } finally {
        if (activo) {
          setLoading(false);
        }
      }
    };

    cargarEstados();

    return () => {
      activo = false;
    };
  }, [pageNumber, pageSize, revision, navigate]);

  const cambiarPagina = (pagina) => {
    setLoading(true);
    setPageNumber(pagina);
  };

  const cambiarTamanoPagina = (tamano) => {
    setLoading(true);
    setPageSize(tamano);
    setPageNumber(1);
  };

  const eliminarEstado = async (estado) => {
    const confirmacion = await Swal.fire({
      title: "Eliminar estado",
      text: `Desea eliminar el estado ${estado.nombre}?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Eliminar",
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#287d7b",
      cancelButtonColor: "#6c757d"
    });

    if (!confirmacion.isConfirmed) return;

    try {
      await deleteEstado(estado.idEstado);

      await Swal.fire({
        icon: "success",
        title: "Estado eliminado",
        text: "El estado fue eliminado correctamente.",
        confirmButtonColor: "#287d7b"
      });

      setLoading(true);
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
        "No se pudo eliminar el estado.";

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
      key: "idEstado",
      header: "Identificador",
      width: "33%"
    },
    {
      key: "nombre",
      header: "Nombre",
      width: "44%"
    },
    {
      key: "acciones",
      header: "Acciones",
      width: "23%",
      render: (estado) => (
        <div className="crud-row-actions">
          <ActionButton
            icon="bi-pencil-square"
            round
            title="Editar"
            onClick={() =>
              navigate(`/estados/editar/${estado.idEstado}`)
            }
          />

          <ActionButton
            icon="bi-trash"
            round
            title="Eliminar"
            onClick={() => eliminarEstado(estado)}
          />
        </div>
      )
    }
  ];

  return (
    <div className="crud-page">
      <ModuleHeader title="Administracion de Estados de Solicitud">
        <ActionButton
          icon="bi-file-earmark-plus"
          onClick={() => navigate("/estados/nuevo")}
        >
          Nuevo
        </ActionButton>
      </ModuleHeader>

      <ModulePanel title="Informacion">
        {error && (
          <div className="alert alert-danger d-flex justify-content-between align-items-center gap-3">
            <span>{error}</span>
            <ActionButton
              onClick={() => {
                setLoading(true);
                setRevision((actual) => actual + 1);
              }}
            >
              Reintentar
            </ActionButton>
          </div>
        )}

        <DataTable
          columns={columns}
          rows={error ? [] : estados}
          keyField="idEstado"
          loading={loading}
          emptyMessage="No hay estados registrados."
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

export default Estados;
