using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface INotificacionEstadoService
    {
        Task<NotificationResult> NotificarCambioEstadoAsync(
            int idSolicitud,
            int idEstadoAnterior,
            int idEstadoNuevo,
            int idUsuarioEjecutor,
            string usuario);
    }
}
