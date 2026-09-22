using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface INotificacionEstadoService
    {
        // Notifica al usuario sobre el cambio de estado de la solicitud.
        Task<NotificationResult> NotificarCambioEstadoAsync(int idSolicitud, int idEstadoAnterior, int idEstadoNuevo, string usuario);
    }
}
