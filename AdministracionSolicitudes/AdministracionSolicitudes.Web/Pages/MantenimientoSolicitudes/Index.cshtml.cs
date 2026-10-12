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
        private readonly IVencimientoService _vencimientoService;

        public IndexModel(ISolicitudService solicitudService, IVencimientoService vencimientoService)
        {
            _solicitudService = solicitudService;
            _vencimientoService = vencimientoService;
        }

        public IEnumerable<Solicitud> Solicitudes { get; set; } = Enumerable.Empty<Solicitud>();
        public Dictionary<int, EstadoVencimiento> EstadosVencimiento { get; set; } = new();

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

            EstadosVencimiento = await _vencimientoService.CalcularEstadosAsync(Solicitudes, idUsuarioActual);
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