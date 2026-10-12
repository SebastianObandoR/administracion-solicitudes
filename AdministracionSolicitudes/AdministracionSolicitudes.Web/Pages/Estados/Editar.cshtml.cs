using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Estados
{
    public class EditarModel : PageModel
    {
        private readonly IEstadoSolicitudService _estadoService;

        public EditarModel(IEstadoSolicitudService estadoService)
        {
            _estadoService = estadoService;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public EstadoSolicitudInputModel Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var estado = await _estadoService.ObtenerPorIdAsync(id, UsuarioId);
            if (estado is null)
            {
                return NotFound();
            }

            CargarFormulario(estado);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var estado = new EstadoSolicitud { IdEstado = Id, Nombre = Input.Nombre.Trim() };
            var resultado = await _estadoService.ActualizarAsync(estado, UsuarioId);

            switch (resultado)
            {
                case ActualizarEstadoSolicitudResultado.Actualizado:
                    TempData["MensajeExito"] = "Estado actualizado correctamente.";
                    return RedirectToPage("Index");
                case ActualizarEstadoSolicitudResultado.NombreDuplicado:
                    ModelState.AddModelError("Input.Nombre", "Ya existe un estado con ese nombre.");
                    return Page();
                case ActualizarEstadoSolicitudResultado.NoEncontrado:
                    return NotFound();
                default:
                    throw new InvalidOperationException("El resultado de actualización no es válido.");
            }
        }

        private void CargarFormulario(EstadoSolicitud estado)
        {
            Id = estado.IdEstado;
            Input = new EstadoSolicitudInputModel { Nombre = estado.Nombre };
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
