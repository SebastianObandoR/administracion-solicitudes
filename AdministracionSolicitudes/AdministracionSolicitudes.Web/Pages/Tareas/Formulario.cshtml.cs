using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Tareas
{
    public class FormularioModel : PageModel
    {
        private readonly ITareaService _tareaService;

        public FormularioModel(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [BindProperty(SupportsGet = true)]
        public int IdSolicitud { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? IdTarea { get; set; }

        [BindProperty]
        public TareaInputModel Entrada { get; set; } = new();

        public SolicitudResumen Solicitud { get; private set; } = new();
        public IReadOnlyList<Usuario> Usuarios { get; private set; } = new List<Usuario>();
        public string? MensajeError { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var resultado = await _tareaService.ObtenerFormularioAsync(IdSolicitud, IdTarea, ObtenerIdUsuarioSesion());

            if (!resultado.IsSuccess || resultado.Data == null) return NotFound(resultado.Message);

            Solicitud = resultado.Data.Solicitud;
            Usuarios = resultado.Data.Usuarios;

            if (resultado.Data.Tarea != null)
            {
                Entrada = new TareaInputModel
                {
                    Descripcion = resultado.Data.Tarea.Descripcion,
                    Fecha = resultado.Data.Tarea.Fecha,
                    IdUsuario = resultado.Data.Tarea.IdUsuario,
                    HorasInvertidas = resultado.Data.Tarea.HorasInvertidas
                };
            }
            else
            {
                Entrada.Fecha = DateTime.Today;
                Entrada.IdUsuario = ObtenerIdUsuarioSesion();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int idUsuarioSesion = ObtenerIdUsuarioSesion();
            var formulario = await _tareaService.ObtenerFormularioAsync(IdSolicitud, IdTarea, idUsuarioSesion);

            if (!formulario.IsSuccess || formulario.Data == null) return NotFound(formulario.Message);

            Solicitud = formulario.Data.Solicitud;
            Usuarios = formulario.Data.Usuarios;

            if (!ModelState.IsValid) return Page();

            var tarea = new Tarea
            {
                IdTarea = IdTarea ?? 0,
                IdSolicitud = IdSolicitud,
                IdUsuario = Entrada.IdUsuario!.Value,
                Descripcion = Entrada.Descripcion,
                Fecha = Entrada.Fecha!.Value,
                HorasInvertidas = Entrada.HorasInvertidas!.Value
            };

            var resultado = IdTarea.HasValue
                ? await _tareaService.ActualizarAsync(tarea, idUsuarioSesion)
                : await _tareaService.CrearAsync(tarea, idUsuarioSesion);

            if (!resultado.IsSuccess)
            {
                MensajeError = resultado.Message;
                return Page();
            }

            TempData["Mensaje"] = resultado.Message;
            TempData["TipoMensaje"] = "success";
            return RedirectToPage("/Tareas/Index", new { idSolicitud = IdSolicitud });
        }

        private int ObtenerIdUsuarioSesion()
        {
            return HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
        }
    }
}