using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Models;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IParticipacionSolicitudRepository
    {
        Task<(IReadOnlyList<ParticipacionSolicitud> Participaciones, bool SolicitudTieneAprobada)> GetPorSolicitudAsync(int idSolicitud);
        Task<ParticipacionDetalle?> GetDetalleAsync(int idSolicitud, int idParticipacion);
        Task<EstadoAprobacionParticipacion> AprobarAsync(int idSolicitud, int idParticipacion);
    }
}
