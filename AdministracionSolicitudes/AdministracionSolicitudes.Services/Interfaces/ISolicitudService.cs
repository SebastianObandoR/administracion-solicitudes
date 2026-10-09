using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface ISolicitudService
    {
        Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize, int? idUsuarioActual);
        Task<Solicitud?> GetByIdAsync(int idSolicitud, int? idUsuarioActual);
        Task<SolicitudOperationResult> CreateAsync(Solicitud solicitud, int? idUsuarioActual);
        Task<SolicitudOperationResult> UpdateAsync(Solicitud solicitud, int? idUsuarioActual);
        Task<SolicitudOperationResult> DeleteAsync(int idSolicitud, int? idUsuarioActual);
        Task<EstadoVencimiento> CalcularEstadoVencimientoAsync(Solicitud solicitud);
    }
}
