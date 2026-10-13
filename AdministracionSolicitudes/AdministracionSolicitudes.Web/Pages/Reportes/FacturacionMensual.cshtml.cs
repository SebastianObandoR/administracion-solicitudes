using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages.Reportes
{
    public class FacturacionMensualModel : PageModel
    {
        private readonly IReporteFacturacionService _service;
        public FacturacionMensualModel(IReporteFacturacionService service) => _service = service;

        [BindProperty(SupportsGet = true)] public int? Mes { get; set; }
        [BindProperty(SupportsGet = true)] public int? Anio { get; set; }
        public IEnumerable<ReporteFacturacionMensual> Registros { get; set; } = Enumerable.Empty<ReporteFacturacionMensual>();
        public decimal TotalMonto => Registros.Sum(x => x.Monto);
        public decimal TotalIva => Registros.Sum(x => x.Iva);
        public decimal TotalGeneral => Registros.Sum(x => x.Total);
        public bool Generado { get; set; }
        public string? MensajeError { get; set; }

        public async Task OnGetAsync()
        {
            if (!Mes.HasValue && !Anio.HasValue) return;
            if (!Mes.HasValue || Mes is < 1 or > 12 || !Anio.HasValue || Anio is < 1 or > 9999)
            {
                MensajeError = "Debe indicar un mes y un año validos.";
                return;
            }

            Registros = (await _service.GetMensualAsync(
                Mes.Value, Anio.Value, HttpContext.Session.GetInt32(SessionKeys.UserId) ?? 0)).ToList();
            Generado = true;
        }
    }
}
