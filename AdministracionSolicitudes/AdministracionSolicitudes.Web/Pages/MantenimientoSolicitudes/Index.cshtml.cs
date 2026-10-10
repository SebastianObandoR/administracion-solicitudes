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
            Solicitudes = items;
            TotalPaginas = (int)Math.Ceiling(total / (double)PageSize);
            if (TotalPaginas == 0) TotalPaginas = 1;

            foreach (var solicitud in Solicitudes)
            {
                EstadosVencimiento[solicitud.IdSolicitud] =
                    await _solicitudService.CalcularEstadoVencimientoAsync(solicitud);
            }
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