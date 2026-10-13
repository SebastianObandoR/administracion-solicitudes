using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class NotificacionEstadoService : INotificacionEstadoService
    {
        private const string Modulo = "Notificaciones de solicitudes";
        private readonly INotificacionEstadoRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IAuditoriaService _auditoria;

        public NotificacionEstadoService(INotificacionEstadoRepository repository, IEmailService emailService, IAuditoriaService auditoria)
        {
            _repository = repository;
            _emailService = emailService;
            _auditoria = auditoria;
        }

        public async Task<NotificationResult> NotificarCambioEstadoAsync(int idSolicitud, int idEstadoAnterior, int idEstadoNuevo, string usuario)
        {
            if (idEstadoAnterior == idEstadoNuevo)
                return new NotificationResult { IsSuccess = true, Message = "El estado no cambio." };

            try
            {
                var datos = await _repository.GetDatosAsync(idSolicitud, idEstadoAnterior, idEstadoNuevo);
                if (datos == null)
                    return new NotificationResult { IsSuccess = false, Message = "No se encontraron los datos de la solicitud." };

                string asunto = $"Cambio de estado de la solicitud {datos.ConsecutivoOficio}";
                string cuerpo = $@"Estimado(a) {datos.NombreRepresentante}:

El estado de la solicitud {datos.ConsecutivoOficio} - {datos.Titulo} ha sido cambiado.

Usuario que realizo el cambio: {usuario}
Estado anterior: {datos.EstadoAnterior}
Estado nuevo: {datos.EstadoNuevo}

Administracion de Solicitudes - Publica S.A.";

                await _emailService.SendAsync(datos.CorreoRepresentante, asunto, cuerpo);
                return new NotificationResult { IsSuccess = true, Message = "Notificacion enviada correctamente." };
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(0, Modulo, ex);
                return new NotificationResult { IsSuccess = false, Message = "No se pudo enviar la notificacion por correo." };
            }
        }
    }
}
