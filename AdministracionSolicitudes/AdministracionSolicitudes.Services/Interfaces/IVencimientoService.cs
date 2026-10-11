using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IVencimientoService
    {
        Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor);
    }
}
