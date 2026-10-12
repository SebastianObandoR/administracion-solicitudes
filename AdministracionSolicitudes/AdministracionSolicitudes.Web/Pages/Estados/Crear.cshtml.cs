using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Estados
{
    public class CrearModel : PageModel
    {
        private readonly IEstadoSolicitudService _estadoService;

        public CrearModel(IEstadoSolicitudService estadoService)
        {
            _estadoService = estadoService;
        }

        [BindProperty]
        public EstadoSolicitudInputModel Input { get; set; } = new();

        [TempData]
        public string? MensajeExito { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var estado = new EstadoSolicitud { Nombre = Input.Nombre.Trim() };
            if (!await _estadoService.CrearAsync(estado, UsuarioId))
            {
                ModelState.AddModelError("Input.Nombre", "Ya existe un estado con ese nombre.");
                return Page();
            }

            MensajeExito = "Estado creado correctamente.";
            return RedirectToPage("Index");
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
