using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Representantes
{
    public class EditarModel : PageModel
    {
        private readonly IRepresentanteService _representanteService;

        public EditarModel(IRepresentanteService representanteService)
        {
            _representanteService = representanteService;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public RepresentanteInputModel Input { get; set; } = new();

        [TempData]
        public string? MensajeError { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var representante = await _representanteService.ObtenerPorIdAsync(id, UsuarioId);
            if (representante is null)
            {
                return NotFound();
            }

            CargarFormulario(representante);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (representante, errores) = await _representanteService.GuardarAsync(Input, UsuarioId, Id);
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

            if (representante is null)
            {
                return NotFound();
            }

            MensajeError = null;
            TempData["MensajeExito"] = "Representante actualizado correctamente.";
            return RedirectToPage("Index");
        }

        private void CargarFormulario(Representante representante)
        {
            Id = representante.IdRepresentante;
            Input = new RepresentanteInputModel
            {
                Nombre = representante.Nombre,
                Correo = representante.Correo
            };
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
