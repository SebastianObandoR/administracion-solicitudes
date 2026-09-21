using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public IActionResult OnGet(string? message = null)
        {
            if (HttpContext.Session.GetInt32(SessionKeys.UserId).HasValue) return RedirectToPage("/Index");

            Message = message ?? string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _authenticationService.AuthenticateAsync(Input.Username, Input.Password);

            if (!result.IsSuccess || result.User == null)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            HttpContext.Session.SetInt32(SessionKeys.UserId, result.User.IdUsuario);
            HttpContext.Session.SetString(SessionKeys.Username, result.User.NombreUsuario);
            HttpContext.Session.SetString(SessionKeys.FullName, result.User.NombreCompleto);

            return RedirectToPage("/Index");
        }
    }
}