using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Solicitudes
{
    public class IndexModel : PageModel
    {
        private const int PageSize = 5; // Criterio de aceptacion SOL1: maximo 5 solicitudes por pagina.

        private readonly ISolicitudService _solicitudService;

        public IndexModel(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        public IEnumerable<Solicitud> Solicitudes { get; set; } = Enumerable.Empty<Solicitud>();
        public Dictionary<int, EstadoVencimiento> EstadosVencimiento { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPaginas { get; set; }
        public string? MensajeExito { get; set; }
        public string? MensajeError { get; set; }

        public async Task OnGetAsync(string? mensajeExito = null, string? mensajeError = null)
        {
            if (PageNumber < 1) PageNumber = 1;

            int? idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId);

            var (items, total) = await _solicitudService.GetPagedAsync(PageNumber, PageSize, idUsuarioActual);
            Solicitudes = items;
            TotalPaginas = (int)Math.Ceiling(total / (double)PageSize);
            if (TotalPaginas == 0) TotalPaginas = 1;

            foreach (var solicitud in Solicitudes)
            {
                EstadosVencimiento[solicitud.IdSolicitud] = await _solicitudService.CalcularEstadoVencimientoAsync(solicitud);
            }

            MensajeExito = mensajeExito;
            MensajeError = mensajeError;
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idSolicitud)
        {
            int? idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId);

            var resultado = await _solicitudService.DeleteAsync(idSolicitud, idUsuarioActual);

            if (!resultado.IsSuccess)
            {
                return RedirectToPage(new { PageNumber, mensajeError = resultado.Message });
            }

            return RedirectToPage(new { PageNumber, mensajeExito = "Solicitud eliminada correctamente." });
        }
    }
}
