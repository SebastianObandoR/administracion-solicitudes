using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Usuarios
{
    public class EditarModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public EditarModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public int IdUsuario { get; set; }

        [BindProperty]
        public UsuarioEntrada Usuario { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int idUsuario)
        {
            var usuario = await _usuarioService.ObtenerPorIdAsync(idUsuario, ObtenerIdUsuarioSesion());
            if (usuario == null) return RedirectToPage("/Usuarios/Index");

            IdUsuario = usuario.IdUsuario;
            Usuario.NombreUsuario = usuario.NombreUsuario;
            Usuario.NombreCompleto = usuario.NombreCompleto;
            Usuario.Correo = usuario.Correo;
            Usuario.Estado = usuario.Estado;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resultado = await _usuarioService.ActualizarAsync(IdUsuario, Usuario, ObtenerIdUsuarioSesion());

            if (!resultado.Exitoso)
            {
                TempData["MensajeFormulario"] = resultado.Mensaje;
                return Page();
            }

            TempData["Mensaje"] = resultado.Mensaje;
            TempData["TipoMensaje"] = "success";
            return RedirectToPage("/Usuarios/Index");
        }

        private int ObtenerIdUsuarioSesion()
        {
            return HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
        }
    }
}