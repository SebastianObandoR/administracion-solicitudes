using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IParticipacionSolicitudService
    {
        Task<(IReadOnlyList<ParticipacionSolicitud> Participaciones, bool SolicitudTieneAprobada)> GetPorSolicitudAsync(int idSolicitud, int idUsuario);
        Task<ParticipacionDetalle?> GetDetalleAsync(int idSolicitud, int idParticipacion, int idUsuario);
        Task<ResultadoOperacion> AprobarAsync(int idSolicitud, int idParticipacion, int idUsuario);
    }
}
