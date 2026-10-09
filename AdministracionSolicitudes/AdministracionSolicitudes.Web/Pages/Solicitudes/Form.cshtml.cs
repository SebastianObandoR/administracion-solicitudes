using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministracionSolicitudes.Web.Pages.Solicitudes
{
    public class FormModel : PageModel
    {
        private readonly ISolicitudService _solicitudService;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IEstadoSolicitudRepository _estadoSolicitudRepository;

        public FormModel(
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
        public bool EsEdicion => Input.IdSolicitud > 0;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await CargarRepresentantesAsync();

            if (id.HasValue)
            {
                await CargarEstadosAsync();

                int? idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId);
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
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarRepresentantesAsync();
                if (EsEdicion) await CargarEstadosAsync();
                return Page();
            }

            int? idUsuarioActual = HttpContext.Session.GetInt32(SessionKeys.UserId);

            var solicitud = new Solicitud
            {
                IdSolicitud = Input.IdSolicitud,
                ConsecutivoOficio = Input.ConsecutivoOficio.Trim(),
                Titulo = Input.Titulo.Trim(),
                Descripcion = Input.Descripcion,
                Observaciones = Input.Observaciones,
                DocumentoInicio = Input.DocumentoInicio,
                DocumentoRespuesta = Input.DocumentoRespuesta,
                IdRepresentante = Input.IdRepresentante,
                IdEstado = Input.IdEstado, 
                FechaIngreso = Input.FechaIngreso,
                FechaRespuesta = Input.FechaRespuesta,
                FechaInicio = Input.FechaInicio
            };

            var resultado = EsEdicion
                ? await _solicitudService.UpdateAsync(solicitud, idUsuarioActual)
                : await _solicitudService.CreateAsync(solicitud, idUsuarioActual);

            if (!resultado.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, resultado.Message);
                await CargarRepresentantesAsync();
                if (EsEdicion) await CargarEstadosAsync();
                return Page();
            }

            string accion = EsEdicion ? "actualizada" : "creada";
            return RedirectToPage("./Index", new { mensajeExito = $"Solicitud {accion} correctamente." });
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
