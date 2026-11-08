using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Repository.Models;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class ParticipacionSolicitudService : IParticipacionSolicitudService
    {
        private const string Modulo = "Participaciones de solicitudes";
        private readonly IParticipacionSolicitudRepository _repository;
        private readonly IAuditoriaService _auditoria;

        public ParticipacionSolicitudService(IParticipacionSolicitudRepository repository, IAuditoriaService auditoria)
        {
            _repository = repository;
            _auditoria = auditoria;
        }

        public async Task<(IReadOnlyList<AdministracionSolicitudes.Entities.ParticipacionSolicitud> Participaciones, bool SolicitudTieneAprobada)> GetPorSolicitudAsync(int idSolicitud, int idUsuario)
        {
            try
            {
                var resultado = await _repository.GetPorSolicitudAsync(idSolicitud);
                await _auditoria.RegistrarConsultaAsync(idUsuario, Modulo, "participaciones de la solicitud");
                return resultado;
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                throw;
            }
        }

        public async Task<AdministracionSolicitudes.Entities.ParticipacionDetalle?> GetDetalleAsync(int idSolicitud, int idParticipacion, int idUsuario)
        {
            try
            {
                var detalle = await _repository.GetDetalleAsync(idSolicitud, idParticipacion);
                if (detalle != null)
                    await _auditoria.RegistrarConsultaAsync(idUsuario, Modulo, "detalle de propuesta del proveedor");
                return detalle;
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                throw;
            }
        }

        public async Task<ResultadoOperacion> AprobarAsync(int idSolicitud, int idParticipacion, int idUsuario)
        {
            try
            {
                var estado = await _repository.AprobarAsync(idSolicitud, idParticipacion);
                switch (estado)
                {
                    case EstadoAprobacionParticipacion.Aprobada:
                        await _auditoria.RegistrarActualizacionAsync(idUsuario, Modulo,
                            new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion, Aprobada = false },
                            new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion, Aprobada = true });
                        return ResultadoOperacion.Exito("La propuesta fue aprobada y sus desgloses quedaron asociados a la solicitud.");
                    case EstadoAprobacionParticipacion.SolicitudNoEncontrada:
                        return ResultadoOperacion.Error("La solicitud indicada no existe.");
                    case EstadoAprobacionParticipacion.ParticipacionNoEncontrada:
                        return ResultadoOperacion.Error("La participación indicada no existe para esta solicitud.");
                    case EstadoAprobacionParticipacion.SolicitudYaTieneAprobada:
                        return ResultadoOperacion.Error("Esta solicitud ya tiene una propuesta aprobada.");
                    case EstadoAprobacionParticipacion.PropuestaSinDesgloses:
                        return ResultadoOperacion.Error("No se puede aprobar una propuesta sin desgloses.");
                    default:
                        return ResultadoOperacion.Error("No se pudo aprobar la propuesta.");
                }
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                return ResultadoOperacion.Error("Ocurrió un error al aprobar la propuesta.");
            }
        }
    }
}
