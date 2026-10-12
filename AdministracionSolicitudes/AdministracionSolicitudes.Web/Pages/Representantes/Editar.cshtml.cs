using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
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

            var representante = new Representante
            {
                IdRepresentante = Id,
                Nombre = Input.Nombre.Trim(),
                Correo = Input.Correo.Trim()
            };

            var resultado = await _representanteService.ActualizarAsync(representante, UsuarioId);
            switch (resultado)
            {
                case ActualizarRepresentanteResultado.Actualizado:
                    MensajeError = null;
                    TempData["MensajeExito"] = "Representante actualizado correctamente.";
                    return RedirectToPage("Index");
                case ActualizarRepresentanteResultado.CorreoDuplicado:
                    ModelState.AddModelError("Input.Correo", "Ya existe un representante con ese email.");
                    return Page();
                case ActualizarRepresentanteResultado.NoEncontrado:
                    return NotFound();
                default:
                    throw new InvalidOperationException("El resultado de actualización no es válido.");
            }
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
