using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministracionSolicitudes.Web.Pages.MantenimientoSolicitudes
{
    public class CrearModel : PageModel
    {
        private readonly ISolicitudService _solicitudService;

        public CrearModel(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [BindProperty]
        public SolicitudInputModel Input { get; set; } = new();

        public List<SelectListItem> Representantes { get; set; } = new();

        public List<string> ErroresValidacion { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            await CargarRepresentantesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarRepresentantesAsync();
                ErroresValidacion = ObtenerErroresDeValidacion();
                return Page();
            }

            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;

            var solicitud = new Solicitud
            {
                ConsecutivoOficio = Input.ConsecutivoOficio ?? string.Empty,
                Titulo = Input.Titulo ?? string.Empty,
                Descripcion = Input.Descripcion,
                Observaciones = Input.Observaciones,
                DocumentoInicio = Input.DocumentoInicio,
                DocumentoRespuesta = Input.DocumentoRespuesta,
                IdRepresentante = Input.IdRepresentante.GetValueOrDefault(),
                FechaIngreso = Input.FechaIngreso.GetValueOrDefault(),
                FechaRespuesta = Input.FechaRespuesta,
                FechaInicio = Input.FechaInicio
            };

            var resultado = await _solicitudService.CreateAsync(solicitud, idUsuarioActual);

            if (!resultado.IsSuccess)
            {
                foreach (var campo in resultado.ValidationErrors)
                    foreach (var mensaje in campo.Value)
                        ModelState.AddModelError($"Input.{campo.Key}", mensaje);
                if (!string.IsNullOrWhiteSpace(resultado.Message))
                    ModelState.AddModelError(string.Empty, resultado.Message);
                await CargarRepresentantesAsync();
                ErroresValidacion = ObtenerErroresDeValidacion();
                return Page();
            }

            TempData["Mensaje"] = "Solicitud creada correctamente.";
            TempData["TipoMensaje"] = "success";
            return RedirectToPage("./Index");
        }

        private List<string> ObtenerErroresDeValidacion()
        {
            return ModelState
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Key) && kv.Value?.Errors.Count > 0)
                .SelectMany(kv => kv.Value!.Errors)
                .Select(e => e.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .ToList();
        }

        private async Task CargarRepresentantesAsync()
        {
            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var representantes = await _solicitudService.GetRepresentantesAsync(idUsuarioActual);
            Representantes = representantes
                .Select(r => new SelectListItem(r.Nombre, r.IdRepresentante.ToString()))
                .ToList();
        }
    }
}
