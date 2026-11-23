using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
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

            var (_, errores) = await _representanteService.GuardarAsync(Input, UsuarioId);
            if (errores.Count > 0)
            {
                foreach (var (campo, mensajes) in errores)
                {
                    foreach (var mensaje in mensajes)
                    {
                        ModelState.AddModelError($"Input.{campo}", mensaje);
                    }
                }
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
