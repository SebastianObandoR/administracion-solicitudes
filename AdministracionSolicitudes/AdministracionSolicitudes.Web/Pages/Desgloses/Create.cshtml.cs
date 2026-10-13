using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Desgloses
{
    public class CreateModel : PageModel
    {
        private readonly IDesgloseSolicitudService _service;
        public CreateModel(IDesgloseSolicitudService service) => _service = service;
        [BindProperty] public DesgloseSolicitudInputModel Input { get; set; } = new();
        public List<string> ErroresValidacion { get; set; } = new();

        public IActionResult OnGet(int idSolicitud)
        {
            if (idSolicitud <= 0) return RedirectToPage("/MantenimientoSolicitudes/Index");
            Input.IdSolicitud = idSolicitud;
            Input.Mes = DateTime.Today.Month;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { CargarErrores(); return Page(); }
            var resultado = await _service.CreateAsync(Mapear(), HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0);
            if (!resultado.Exitoso) { ModelState.AddModelError(string.Empty, resultado.Mensaje); CargarErrores(); return Page(); }
            TempData["Mensaje"] = resultado.Mensaje; TempData["TipoMensaje"] = "success";
            return RedirectToPage("./Index", new { Input.IdSolicitud });
        }

        private DesgloseSolicitud Mapear() => new() { IdSolicitud = Input.IdSolicitud, Mes = Input.Mes, Anio = Input.Anio, Horas = Input.Horas, Monto = Input.Monto, Observaciones = Input.Observaciones, PorcentajeCobro = Input.PorcentajeCobro };
        private void CargarErrores() => ErroresValidacion = ModelState.Where(x => !string.IsNullOrWhiteSpace(x.Key)).SelectMany(x => x.Value!.Errors).Select(x => x.ErrorMessage).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
    }
}
