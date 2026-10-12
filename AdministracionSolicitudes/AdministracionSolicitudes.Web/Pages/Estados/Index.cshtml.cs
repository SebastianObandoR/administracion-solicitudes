using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Estados
{
    public class IndexModel : PageModel
    {
        private readonly IEstadoSolicitudService _estadoService;

        public IndexModel(IEstadoSolicitudService estadoService)
        {
            _estadoService = estadoService;
        }

        public EstadoSolicitudPagina Resultado { get; private set; } = new();

        [TempData]
        public string? MensajeExito { get; set; }

        [TempData]
        public string? MensajeError { get; set; }

        public async Task<IActionResult> OnGetAsync(int pagina = 1)
        {
            Resultado = await _estadoService.ObtenerPaginaAsync(pagina, UsuarioId);

            if (Resultado.TotalPaginas > 0 && Resultado.Pagina > Resultado.TotalPaginas)
            {
                return RedirectToPage(new { pagina = Resultado.TotalPaginas });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id, int pagina = 1)
        {
            var resultado = await _estadoService.EliminarAsync(id, UsuarioId);

            switch (resultado)
            {
                case EliminarEstadoSolicitudResultado.Eliminado:
                    MensajeExito = "Estado eliminado correctamente.";
                    break;
                case EliminarEstadoSolicitudResultado.TieneSolicitudes:
                    MensajeError = "No se puede eliminar un registro con datos relacionados.";
                    break;
                case EliminarEstadoSolicitudResultado.NoEncontrado:
                    MensajeError = "No se encontró el estado seleccionado.";
                    break;
            }

            return RedirectToPage(new { pagina });
        }

        private int UsuarioId =>
            HttpContext.Session.GetInt32(SessionKeys.UserId)
            ?? throw new InvalidOperationException("No se encontró el usuario autenticado en la sesión.");
    }
}
