using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.MantenimientoSolicitudes
{
    public class IndexModel : PageModel
    {
        private const int PageSize = 5; // SOL1

        private readonly ISolicitudService _solicitudService;

        public IndexModel(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        public IEnumerable<Solicitud> Solicitudes { get; set; } = Enumerable.Empty<Solicitud>();
        public Dictionary<int, EstadoVencimiento> EstadosVencimiento { get; set; } = new();

        // Expuesto a la vista para mantener fija la altura de la tabla (5 filas)
        public int TamanoPagina => PageSize;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPaginas { get; set; }

        [TempData]
        public string? Mensaje { get; set; }

        [TempData]
        public string? TipoMensaje { get; set; }

        public async Task OnGetAsync()
        {
            if (PageNumber < 1) PageNumber = 1;

            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;

            var (items, total) = await _solicitudService.GetPagedAsync(PageNumber, PageSize, idUsuarioActual);
            Solicitudes = items.ToList();
            TotalPaginas = (int)Math.Ceiling(total / (double)PageSize);
            if (TotalPaginas == 0) TotalPaginas = 1;

            EstadosVencimiento = await _solicitudService.CalcularEstadosVencimientoAsync(Solicitudes, idUsuarioActual);
        }

        public async Task<IActionResult> OnGetDetalleAsync(int id)
        {
            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;

            // GetByIdAsync registra en bitacora "El usuario consulta solicitud"
            var solicitud = await _solicitudService.GetByIdAsync(id, idUsuarioActual);
            if (solicitud == null) return NotFound();

            return new JsonResult(new
            {
                consecutivo = solicitud.ConsecutivoOficio,
                estado = solicitud.NombreEstado,
                titulo = solicitud.Titulo,
                representante = solicitud.NombreRepresentante,
                ingreso = solicitud.FechaIngreso.ToString("dd/MM/yyyy"),
                inicio = solicitud.FechaInicio?.ToString("dd/MM/yyyy"),
                respuesta = solicitud.FechaRespuesta?.ToString("dd/MM/yyyy"),
                docInicio = solicitud.DocumentoInicio,
                docRespuesta = solicitud.DocumentoRespuesta,
                descripcion = solicitud.Descripcion,
                observaciones = solicitud.Observaciones
            });
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idSolicitud)
        {
            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;

            var resultado = await _solicitudService.DeleteAsync(idSolicitud, idUsuarioActual);

            Mensaje = resultado.IsSuccess ? "Solicitud eliminada correctamente." : resultado.Message;
            TipoMensaje = resultado.IsSuccess ? "success" : "error";

            return RedirectToPage(new { PageNumber });
        }
    }
}