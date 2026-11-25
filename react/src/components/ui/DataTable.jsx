
function DataTable({
  columns,
  rows,
  keyField = "id",
  loading = false,
  emptyMessage = "No hay registros disponibles."
}) {
  return (
    <div className="table-responsive">
      <table className="table crud-table">
        <thead>
          <tr>
            {columns.map((column) => (
              <th
                key={column.key}
                style={{ width: column.width }}
              >
                {column.header}
              </th>
            ))}
          </tr>
        </thead>

        <tbody>
          {loading ? (
            <tr>
              <td colSpan={columns.length} className="crud-empty">
                <div className="spinner-border spinner-border-sm me-2"></div>
                Cargando registros...
              </td>
            </tr>
          ) : rows.length === 0 ? (
            <tr>
              <td colSpan={columns.length} className="crud-empty">
                {emptyMessage}
              </td>
            </tr>
          ) : (
            rows.map((row) => (
              <tr key={row[keyField]}>
                {columns.map((column) => (
                  <td key={column.key}>
                    {column.render
                      ? column.render(row)
                      : row[column.key]}
                  </td>
                ))}
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}

export default DataTable;
