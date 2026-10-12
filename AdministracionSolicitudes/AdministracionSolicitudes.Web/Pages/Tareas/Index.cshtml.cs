using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Tareas
{
    public class IndexModel : PageModel
    {
        private readonly ITareaService _tareaService;

        public IndexModel(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        public IReadOnlyList<SolicitudResumen> Solicitudes { get; private set; } = new List<SolicitudResumen>();
        public bool HaySolicitudSeleccionada { get; private set; }
        public SolicitudResumen Solicitud { get; private set; } = new();
        public PagedResult<Tarea> Tareas { get; private set; } = new();

        [TempData]
        public string? Mensaje { get; set; }

        [TempData]
        public string? TipoMensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(int idSolicitud, int pagina = 1)
        {
            int idUsuarioSesion = ObtenerIdUsuarioSesion();

            var listaSolicitudes = await _tareaService.ObtenerSolicitudesAsync(idUsuarioSesion);

            if (!listaSolicitudes.IsSuccess) return StatusCode(500, listaSolicitudes.Message);

            Solicitudes = listaSolicitudes.Data ?? new List<SolicitudResumen>();

            if (idSolicitud <= 0) return Page();

            var resultado = await _tareaService.ObtenerTareasPorSolicitudAsync(idSolicitud, pagina, idUsuarioSesion);

            if (!resultado.IsSuccess || resultado.Data == null) return NotFound(resultado.Message);

            HaySolicitudSeleccionada = true;
            Solicitud = resultado.Data.Solicitud;
            Tareas = resultado.Data.Tareas;
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idTarea, int idSolicitud)
        {
            var resultado = await _tareaService.EliminarAsync(idTarea, ObtenerIdUsuarioSesion());
            Mensaje = resultado.Message;
            TipoMensaje = resultado.IsSuccess ? "success" : "error";
            return RedirectToPage("/Tareas/Index", new { idSolicitud });
        }

        private int ObtenerIdUsuarioSesion()
        {
            return HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
        }
    }
}