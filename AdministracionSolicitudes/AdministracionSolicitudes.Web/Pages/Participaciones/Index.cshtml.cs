using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Participaciones
{
    public class IndexModel : PageModel
    {
        private readonly IParticipacionSolicitudService _participacionService;
        private readonly ISolicitudService _solicitudService;

        public IndexModel(IParticipacionSolicitudService participacionService, ISolicitudService solicitudService)
        {
            _participacionService = participacionService;
            _solicitudService = solicitudService;
        }

        [BindProperty(SupportsGet = true)] public int IdSolicitud { get; set; }
        public Solicitud? Solicitud { get; private set; }
        public IReadOnlyList<ParticipacionSolicitud> Participaciones { get; private set; } = Array.Empty<ParticipacionSolicitud>();
        public bool SolicitudTieneAprobada { get; private set; }
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? TipoMensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (IdSolicitud <= 0) return RedirectToPage("/MantenimientoSolicitudes/Index");
            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            Solicitud = await _solicitudService.GetByIdAsync(IdSolicitud, idUsuario);
            if (Solicitud == null) return RedirectToPage("/MantenimientoSolicitudes/Index");

            var resultado = await _participacionService.GetPorSolicitudAsync(IdSolicitud, idUsuario);
            Participaciones = resultado.Participaciones;
            SolicitudTieneAprobada = resultado.SolicitudTieneAprobada;
            return Page();
        }
    }
}
