using Microsoft.AspNetCore.Mvc;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Reportes
{
    public class TareasPorUsuarioModel : PageModel
    {
        private readonly IReporteTareasUsuarioService _reporteService;

        public TareasPorUsuarioModel(IReporteTareasUsuarioService reporteService)
        {
            _reporteService = reporteService;
        }

        [BindProperty(SupportsGet = true)]
        public int? IdUsuario { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Mes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Anio { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Generar { get; set; }

        public IReadOnlyList<Usuario> Usuarios { get; private set; } = new List<Usuario>();
        public ReporteTareasResultado? Reporte { get; private set; }
        public string? Mensaje { get; private set; }
        public string? TipoMensaje { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int idUsuarioSesion = ObtenerIdUsuarioSesion();

            var usuarios = await _reporteService.ObtenerUsuariosAsync(idUsuarioSesion);

            if (!usuarios.IsSuccess || usuarios.Data == null) return MostrarError(usuarios.Message);

            Usuarios = usuarios.Data;

            if (!Generar)
            {
                Mes ??= DateTime.Today.Month;
                Anio ??= DateTime.Today.Year;
                return Page();
            }

            if (!IdUsuario.HasValue || !Mes.HasValue || !Anio.HasValue)
            {
                return MostrarError("Debe seleccionar el usuario, el mes y el año para generar el reporte.");
            }

            var resultado = await _reporteService.GenerarReporteAsync(IdUsuario.Value, Mes.Value, Anio.Value, idUsuarioSesion);

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
