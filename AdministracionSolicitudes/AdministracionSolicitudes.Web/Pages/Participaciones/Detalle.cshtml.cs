using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Participaciones
{
    public class DetalleModel : PageModel
    {
        private readonly IParticipacionSolicitudService _participacionService;
        private readonly ISolicitudService _solicitudService;

        public DetalleModel(IParticipacionSolicitudService participacionService, ISolicitudService solicitudService)
        {
            _participacionService = participacionService;
            _solicitudService = solicitudService;
        }

        [BindProperty(SupportsGet = true)] public int IdSolicitud { get; set; }
        [BindProperty(SupportsGet = true)] public int IdParticipacion { get; set; }
        public Solicitud? Solicitud { get; private set; }
        public ParticipacionDetalle? Detalle { get; private set; }
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? TipoMensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (IdSolicitud <= 0 || IdParticipacion <= 0)
                return RedirectToPage("/MantenimientoSolicitudes/Index");

            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            Solicitud = await _solicitudService.GetByIdAsync(IdSolicitud, idUsuario);
            if (Solicitud == null) return RedirectToPage("/MantenimientoSolicitudes/Index");

            Detalle = await _participacionService.GetDetalleAsync(IdSolicitud, IdParticipacion, idUsuario);
            if (Detalle == null)
            {
                Mensaje = "No se encontró la participación seleccionada.";
                TipoMensaje = "error";
                return RedirectToPage("./Index", new { idSolicitud = IdSolicitud });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAprobarAsync()
        {
            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var resultado = await _participacionService.AprobarAsync(IdSolicitud, IdParticipacion, idUsuario);
            Mensaje = resultado.Mensaje;
            TipoMensaje = resultado.Exitoso ? "success" : "error";
            return RedirectToPage("./Detalle", new { idSolicitud = IdSolicitud, idParticipacion = IdParticipacion });
        }
    }
}
