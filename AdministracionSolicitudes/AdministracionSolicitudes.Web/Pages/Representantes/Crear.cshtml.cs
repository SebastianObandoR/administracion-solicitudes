using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Representantes
{
    public class CrearModel : PageModel
    {
        private readonly IRepresentanteService _representanteService;

        public CrearModel(IRepresentanteService representanteService)
        {
            _representanteService = representanteService;
        }

        [BindProperty]
        public RepresentanteInputModel Input { get; set; } = new();

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

            var representante = new Representante
            {
                Nombre = Input.Nombre.Trim(),
                Correo = Input.Correo.Trim()
            };

            if (!await _representanteService.CrearAsync(representante, UsuarioId))
            {
                ModelState.AddModelError("Input.Correo", "Ya existe un representante con ese email.");
                return Page();
            }

            MensajeExito = "Representante creado correctamente.";
            return RedirectToPage("Index");
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
