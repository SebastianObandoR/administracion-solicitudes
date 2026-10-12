using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface INotificacionEstadoRepository
    {
        Task<DatosNotificacionEstado?> GetDatosAsync(int idSolicitud, int idEstadoAnterior, int idEstadoNuevo);
    }
}
