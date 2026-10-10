using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministracionSolicitudes.Web.Pages.MantenimientoSolicitudes
{
    public class EditModel : PageModel
    {
        private readonly ISolicitudService _solicitudService;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IEstadoSolicitudRepository _estadoSolicitudRepository;

        public EditModel(
            ISolicitudService solicitudService,
            IRepresentanteRepository representanteRepository,
            IEstadoSolicitudRepository estadoSolicitudRepository)
        {
            _solicitudService = solicitudService;
            _representanteRepository = representanteRepository;
            _estadoSolicitudRepository = estadoSolicitudRepository;
        }

        [BindProperty]
        public SolicitudInputModel Input { get; set; } = new();

        public List<SelectListItem> Representantes { get; set; } = new();
        public List<SelectListItem> Estados { get; set; } = new();

        public List<string> ErroresValidacion { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue) return RedirectToPage("./Index");

            await CargarRepresentantesAsync();
            await CargarEstadosAsync();

            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var solicitud = await _solicitudService.GetByIdAsync(id.Value, idUsuarioActual);

            if (solicitud == null) return RedirectToPage("./Index");

            Input = new SolicitudInputModel
            {
                IdSolicitud = solicitud.IdSolicitud,
                ConsecutivoOficio = solicitud.ConsecutivoOficio,
                Titulo = solicitud.Titulo,
                Descripcion = solicitud.Descripcion,
                Observaciones = solicitud.Observaciones,
                DocumentoInicio = solicitud.DocumentoInicio,
                DocumentoRespuesta = solicitud.DocumentoRespuesta,
                IdRepresentante = solicitud.IdRepresentante,
                IdEstado = solicitud.IdEstado,
                FechaIngreso = solicitud.FechaIngreso,
                FechaRespuesta = solicitud.FechaRespuesta,
                FechaInicio = solicitud.FechaInicio
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {          
            int idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var original = await _solicitudService.GetByIdAsync(Input.IdSolicitud, idUsuarioActual);

            if (original == null)
            {
                return RedirectToPage("./Index");
            }

            ModelState.Remove(nameof(Input.ConsecutivoOficio));
            Input.ConsecutivoOficio = original.ConsecutivoOficio;

            if (!ModelState.IsValid)
            {
                await CargarRepresentantesAsync();
                await CargarEstadosAsync();
                ErroresValidacion = ObtenerErroresDeValidacion();
                return Page();
            }

            var solicitud = new Solicitud
            {
                IdSolicitud = Input.IdSolicitud,
                ConsecutivoOficio = Input.ConsecutivoOficio,
                Titulo = Input.Titulo.Trim(),
                Descripcion = Input.Descripcion,
                Observaciones = Input.Observaciones,
                DocumentoInicio = Input.DocumentoInicio,
                DocumentoRespuesta = Input.DocumentoRespuesta,
                IdRepresentante = Input.IdRepresentante!.Value,
                IdEstado = Input.IdEstado!.Value,
                FechaIngreso = Input.FechaIngreso!.Value,
                FechaRespuesta = Input.FechaRespuesta,
                FechaInicio = Input.FechaInicio
            };

            var resultado = await _solicitudService.UpdateAsync(solicitud, idUsuarioActual);

            if (!resultado.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, resultado.Message);
                await CargarRepresentantesAsync();
                await CargarEstadosAsync();
                ErroresValidacion = ObtenerErroresDeValidacion();
                return Page();
            }

            TempData["Mensaje"] = "Solicitud actualizada correctamente.";
            TempData["TipoMensaje"] = "success";
            return RedirectToPage("./Index");
        }

        private List<string> ObtenerErroresDeValidacion()
        {
            return ModelState
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Key) && kv.Value.Errors.Count > 0)
                .SelectMany(kv => kv.Value.Errors)
                .Select(e => e.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .ToList();
        }

        private async Task CargarRepresentantesAsync()
        {
            var representantes = await _representanteRepository.GetAllAsync();
            Representantes = representantes
                .Select(r => new SelectListItem(r.Nombre, r.IdRepresentante.ToString()))
                .ToList();
        }

        private async Task CargarEstadosAsync()
        {
            var estados = await _estadoSolicitudRepository.GetAllAsync();
            Estados = estados
                .Select(e => new SelectListItem(e.Nombre, e.IdEstado.ToString()))
                .ToList();
        }
    }
}