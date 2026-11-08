using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdministracionSolicitudes.Web.Pages
{
    public class DetalleSolicitudModel : PageModel
    {
        private readonly ISolicitudRepository _solicitudRepository;

        public DetalleSolicitudModel(ISolicitudRepository solicitudRepository)
        {
            _solicitudRepository = solicitudRepository;
        }

        [BindProperty(SupportsGet = true)]
        public int IdSolicitud { get; set; }

        public Solicitud? Solicitud { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (IdSolicitud <= 0)
            {
                return NotFound();
            }

            Solicitud = await _solicitudRepository.GetPublicadaVigenteByIdAsync(IdSolicitud);
            return Solicitud is null ? NotFound() : Page();
        }
    }
}
