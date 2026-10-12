using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Desgloses
{
    public class IndexModel : PageModel
    {
        private const int PageSize = 10;
        private readonly IDesgloseSolicitudService _desgloseService;
        private readonly ISolicitudService _solicitudService;

        public IndexModel(IDesgloseSolicitudService desgloseService, ISolicitudService solicitudService)
        {
            _desgloseService = desgloseService;
            _solicitudService = solicitudService;
        }

        [BindProperty(SupportsGet = true)] public int IdSolicitud { get; set; }
        [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;
        public IEnumerable<DesgloseSolicitud> Desgloses { get; set; } = Enumerable.Empty<DesgloseSolicitud>();
        public Solicitud? Solicitud { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina => PageSize;
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? TipoMensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (IdSolicitud <= 0) return RedirectToPage("/MantenimientoSolicitudes/Index");
            if (PageNumber < 1) PageNumber = 1;
            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            Solicitud = await _solicitudService.GetByIdAsync(IdSolicitud, idUsuario);
            if (Solicitud == null) return RedirectToPage("/MantenimientoSolicitudes/Index");

            var (items, total) = await _desgloseService.GetPagedAsync(IdSolicitud, PageNumber, PageSize, idUsuario);
            Desgloses = items.ToList();
            TotalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idSolicitud, int mes, int anio, int pageNumber = 1)
        {
            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var resultado = await _desgloseService.DeleteAsync(idSolicitud, mes, anio, idUsuario);
            Mensaje = resultado.Mensaje;
            TipoMensaje = resultado.Exitoso ? "success" : "error";
            return RedirectToPage(new { idSolicitud, pageNumber });
        }
    }
}
