using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface ISolicitudService
    {
        Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize, int idUsuarioEjecutor);
        Task<Solicitud?> GetByIdAsync(int idSolicitud, int idUsuarioEjecutor);
        Task<SolicitudOperationResult> CreateAsync(Solicitud solicitud, int idUsuarioEjecutor);
        Task<SolicitudOperationResult> UpdateAsync(Solicitud solicitud, int idUsuarioEjecutor);
        Task<SolicitudOperationResult> DeleteAsync(int idSolicitud, int idUsuarioEjecutor);
        Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosVencimientoAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor);
    }
}
