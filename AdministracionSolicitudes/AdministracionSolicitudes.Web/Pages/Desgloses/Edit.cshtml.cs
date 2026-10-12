using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using AdministracionSolicitudes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Desgloses
{
    public class EditModel : PageModel
    {
        private readonly IDesgloseSolicitudService _service;
        public EditModel(IDesgloseSolicitudService service) => _service = service;
        [BindProperty] public DesgloseSolicitudInputModel Input { get; set; } = new();
        public List<string> ErroresValidacion { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int idSolicitud, int mes, int anio)
        {
            int idUsuario = HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0;
            var d = await _service.GetByIdAsync(idSolicitud, mes, anio, idUsuario);
            if (d == null) return RedirectToPage("./Index", new { idSolicitud });
            Input = new() { IdSolicitud = d.IdSolicitud, OriginalMes = d.Mes, OriginalAnio = d.Anio, Mes = d.Mes, Anio = d.Anio, Horas = d.Horas, Monto = d.Monto, Observaciones = d.Observaciones, PorcentajeCobro = d.PorcentajeCobro };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { CargarErrores(); return Page(); }
            var d = new DesgloseSolicitud { IdSolicitud = Input.IdSolicitud, Mes = Input.Mes, Anio = Input.Anio, Horas = Input.Horas, Monto = Input.Monto, Observaciones = Input.Observaciones, PorcentajeCobro = Input.PorcentajeCobro };
            var resultado = await _service.UpdateAsync(d, Input.OriginalMes, Input.OriginalAnio, HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0);
            if (!resultado.Exitoso) { ModelState.AddModelError(string.Empty, resultado.Mensaje); CargarErrores(); return Page(); }
            TempData["Mensaje"] = resultado.Mensaje; TempData["TipoMensaje"] = "success";
            return RedirectToPage("./Index", new { Input.IdSolicitud });
        }
        private void CargarErrores() => ErroresValidacion = ModelState.Where(x => !string.IsNullOrWhiteSpace(x.Key)).SelectMany(x => x.Value!.Errors).Select(x => x.ErrorMessage).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
    }
}
