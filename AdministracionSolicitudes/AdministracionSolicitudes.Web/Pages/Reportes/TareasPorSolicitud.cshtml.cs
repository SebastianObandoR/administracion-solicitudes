using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Reportes
{
    public class TareasPorSolicitudModel : PageModel
    {
        private readonly IReporteTareasSolicitudService _reporteService;

        public TareasPorSolicitudModel(IReporteTareasSolicitudService reporteService)
        {
            _reporteService = reporteService;
        }

        [BindProperty(SupportsGet = true)]
        public int? IdSolicitud { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Mes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Anio { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Generar { get; set; }

        public IReadOnlyList<SolicitudResumen> Solicitudes { get; private set; } = new List<SolicitudResumen>();
        public ReporteTareasResultado? Reporte { get; private set; }
        public string? Mensaje { get; private set; }
        public string? TipoMensaje { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int idUsuarioSesion = ObtenerIdUsuarioSesion();

            var solicitudes = await _reporteService.ObtenerSolicitudesAsync(idUsuarioSesion);

            if (!solicitudes.IsSuccess || solicitudes.Data == null) return MostrarError(solicitudes.Message);

            Solicitudes = solicitudes.Data;

            if (!Generar)
            {
                Mes ??= DateTime.Today.Month;
                Anio ??= DateTime.Today.Year;
                return Page();
            }

            if (!IdSolicitud.HasValue || !Mes.HasValue || !Anio.HasValue)
            {
                return MostrarError("Debe seleccionar la solicitud, el mes y el año para generar el reporte.");
            }

            var resultado = await _reporteService.GenerarReporteAsync(IdSolicitud.Value, Mes.Value, Anio.Value, idUsuarioSesion);

            if (!resultado.IsSuccess || resultado.Data == null) return MostrarError(resultado.Message);

            Reporte = resultado.Data;
            return Page();
        }

        private IActionResult MostrarError(string mensaje)
        {
            Mensaje = mensaje;
            TipoMensaje = "error";
            return Page();
        }

        private int ObtenerIdUsuarioSesion()
        {
            return HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
        }
    }
}