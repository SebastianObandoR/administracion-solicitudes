using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Usuarios
{
    public class CrearModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public CrearModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public UsuarioEntrada Usuario { get; set; } = new();

        public void OnGet()
        {
            Usuario.Estado = EstadoUsuario.Activo;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resultado = await _usuarioService.CrearAsync(Usuario, ObtenerIdUsuarioSesion());

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