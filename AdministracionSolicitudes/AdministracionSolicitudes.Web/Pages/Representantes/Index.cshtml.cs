using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Representantes
{
    public class IndexModel : PageModel
    {
        private readonly IRepresentanteService _representanteService;

        public IndexModel(IRepresentanteService representanteService)
        {
            _representanteService = representanteService;
        }

        public RepresentantePagina Resultado { get; private set; } = new();

        [TempData]
        public string? MensajeExito { get; set; }

        [TempData]
        public string? MensajeError { get; set; }

        public async Task<IActionResult> OnGetAsync(int pagina = 1)
        {
            Resultado = await _representanteService.ObtenerPaginaAsync(pagina, UsuarioId);

            if (Resultado.TotalPaginas > 0 && Resultado.Pagina > Resultado.TotalPaginas)
            {
                return RedirectToPage(new { pagina = Resultado.TotalPaginas });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id, int pagina = 1)
        {
            var resultado = await _representanteService.EliminarAsync(id, UsuarioId);

            switch (resultado)
            {
                case EliminarRepresentanteResultado.Eliminado:
                    MensajeExito = "Representante eliminado correctamente.";
                    break;
                case EliminarRepresentanteResultado.TieneSolicitudes:
                    MensajeError = "No se puede eliminar un registro con datos relacionados.";
                    break;
                case EliminarRepresentanteResultado.NoEncontrado:
                    MensajeError = "No se encontró el representante seleccionado.";
                    break;
            }

            return RedirectToPage(new { pagina });
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
