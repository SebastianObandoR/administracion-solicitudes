using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public IndexModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IEnumerable<Usuario> Usuarios { get; private set; } = [];
        public int PaginaActual { get; private set; }
        public int TotalPaginas { get; private set; }

        [TempData]
        public string? Mensaje { get; set; }

        [TempData]
        public string? TipoMensaje { get; set; }

        public async Task OnGetAsync(int pagina = 1)
        {
            var resultado = await _usuarioService.ListarAsync(pagina, ObtenerIdUsuarioSesion());
            Usuarios = resultado.Usuarios;
            PaginaActual = resultado.PaginaActual;
            TotalPaginas = resultado.TotalPaginas;
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idUsuario)
        {
            var resultado = await _usuarioService.EliminarAsync(idUsuario, ObtenerIdUsuarioSesion());
            Mensaje = resultado.Mensaje;
            TipoMensaje = resultado.Exitoso ? "success" : "error";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int idUsuario, EstadoUsuario estado)
        {
            var resultado = await _usuarioService.CambiarEstadoAsync(idUsuario, estado, ObtenerIdUsuarioSesion());
            Mensaje = resultado.Mensaje;
            TipoMensaje = resultado.Exitoso ? "success" : "error";
            return RedirectToPage();
        }

        private int ObtenerIdUsuarioSesion()
        {
            return HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
        }
    }
}