using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string FullName { get; private set; } = string.Empty;

        public void OnGet()
        {
            FullName = HttpContext.Session.GetString(SessionKeys.FullName) ?? "Usuario";
        }
    }
}