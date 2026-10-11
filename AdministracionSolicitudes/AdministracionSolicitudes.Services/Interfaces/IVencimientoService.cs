using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IVencimientoService
    {
        // SOL1: semaforo del listado
        Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor);

        Task<int> ProcesarVencimientosAsync();
    }
}
