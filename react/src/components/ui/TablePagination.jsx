
function TablePagination({
  pageNumber,
  pageSize,
  totalCount,
  totalPages,
  onPageChange,
  onPageSizeChange
}) {
  const paginas = Math.max(1, totalPages);
  const inicio = totalCount === 0 ? 0 : (pageNumber - 1) * pageSize + 1;
  const fin = Math.min(pageNumber * pageSize, totalCount);

  const primeraPagina = Math.max(1, pageNumber - 2);
  const ultimaPagina = Math.min(paginas, primeraPagina + 4);
  const desde = Math.max(1, ultimaPagina - 4);

  const numeros = [];

  for (let i = desde; i <= ultimaPagina; i++) {
    numeros.push(i);
  }

  return (
    <div className="crud-pagination">
      <div className="crud-pagination-info">
        Mostrando {inicio} - {fin} de {totalCount} registros
      </div>

      <div className="crud-pagination-controls">
        <select
          className="form-select form-select-sm"
          value={pageSize}
          onChange={(event) =>
            onPageSizeChange(Number(event.target.value))
          }
          aria-label="Registros por pagina"
        >
          <option value={5}>5 registros</option>
          <option value={10}>10 registros</option>
          <option value={20}>20 registros</option>
        </select>

        <button
          type="button"
          className="crud-page-button"
          disabled={pageNumber === 1}
          onClick={() => onPageChange(pageNumber - 1)}
          aria-label="Pagina anterior"
        >
          <i className="bi bi-chevron-left"></i>
        </button>

        {numeros.map((numero) => (
          <button
            type="button"
            key={numero}
            className={`crud-page-button ${pageNumber === numero ? "active" : ""}`}
            onClick={() => onPageChange(numero)}
          >
            {numero}
          </button>
        ))}

        <button
          type="button"
          className="crud-page-button"
          disabled={pageNumber >= paginas}
          onClick={() => onPageChange(pageNumber + 1)}
          aria-label="Pagina siguiente"
        >
          <i className="bi bi-chevron-right"></i>
        </button>
      </div>
    </div>
  );
}

export default TablePagination;
