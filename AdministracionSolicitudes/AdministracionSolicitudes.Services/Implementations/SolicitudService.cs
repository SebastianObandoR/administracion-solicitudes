using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class SolicitudService : ISolicitudService
    {
        private const string Modulo = "Solicitudes";
        private const string MensajeNoEliminar = "No se puede eliminar un registro con datos relacionados.";
        private const string MensajeConsecutivoDuplicado = "Ya existe una solicitud con ese consecutivo de oficio.";

        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IEstadoSolicitudRepository _estadoSolicitudRepository;
        private readonly IAuditoriaService _auditoriaService;

        public SolicitudService(
            ISolicitudRepository solicitudRepository,
            IRepresentanteRepository representanteRepository,
            IEstadoSolicitudRepository estadoSolicitudRepository,
            IAuditoriaService auditoriaService)
        {
            _solicitudRepository = solicitudRepository;
            _representanteRepository = representanteRepository;
            _estadoSolicitudRepository = estadoSolicitudRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize, int idUsuarioEjecutor)
        {
            try
            {
                var result = await _solicitudRepository.GetPagedAsync(pageNumber, pageSize);
                await _auditoriaService.RegistrarConsultaAsync(idUsuarioEjecutor, Modulo, "solicitudes");
                return result;
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                throw;
            }
        }

        public async Task<Solicitud?> GetByIdAsync(int idSolicitud, int idUsuarioEjecutor)
        {
            try
            {
                var solicitud = await _solicitudRepository.GetByIdAsync(idSolicitud);
                if (solicitud != null)
                {
                    await _auditoriaService.RegistrarConsultaAsync(idUsuarioEjecutor, Modulo, "solicitud");
                }
                return solicitud;
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                throw;
            }
        }

        public async Task<IEnumerable<Representante>> GetRepresentantesAsync(int idUsuarioEjecutor)
        {
            try
            {
                return await _representanteRepository.GetAllAsync();
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                throw;
            }
        }

        public async Task<IEnumerable<EstadoSolicitud>> GetEstadosAsync(int idUsuarioEjecutor)
        {
            try
            {
                return await _estadoSolicitudRepository.GetAllAsync();
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                throw;
            }
        }

        public async Task<SolicitudOperationResult> CreateAsync(Solicitud solicitud, int idUsuarioEjecutor)
        {
            try
            {
                if (await _solicitudRepository.ExistsConsecutivoAsync(solicitud.ConsecutivoOficio))
                {
                    return SolicitudOperationResult.Failure(MensajeConsecutivoDuplicado);
                }

                // Las solicitudes se crean por defecto en estado "Nueva"
                var estadoNueva = await ObtenerEstadoPorNombreAsync(EstadoSolicitudNombre.Nueva);
                solicitud.IdEstado = estadoNueva.IdEstado;

                int idSolicitud = await _solicitudRepository.CreateAsync(solicitud);
                solicitud.IdSolicitud = idSolicitud;

                await _auditoriaService.RegistrarCreacionAsync(idUsuarioEjecutor, Modulo, ToBitacoraPayload(solicitud));

                return SolicitudOperationResult.Success(idSolicitud);
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                return SolicitudOperationResult.Failure("Ocurrio un error al crear la solicitud.");
            }
        }

        public async Task<SolicitudOperationResult> UpdateAsync(Solicitud solicitud, int idUsuarioEjecutor)
        {
            try
            {
                var existente = await _solicitudRepository.GetByIdAsync(solicitud.IdSolicitud);
                if (existente == null)
                {
                    return SolicitudOperationResult.Failure("La solicitud no existe.");
                }

                solicitud.ConsecutivoOficio = existente.ConsecutivoOficio;

                var registroAnterior = ToBitacoraPayload(existente);
                await _solicitudRepository.UpdateAsync(solicitud);
                var actualizado = await _solicitudRepository.GetByIdAsync(solicitud.IdSolicitud);
                var registroActual = ToBitacoraPayload(actualizado!);

                await _auditoriaService.RegistrarActualizacionAsync(idUsuarioEjecutor, Modulo, registroAnterior, registroActual);
                return SolicitudOperationResult.Success(solicitud.IdSolicitud);
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                return SolicitudOperationResult.Failure("Ocurrio un error al actualizar la solicitud.");
            }
        }

        public async Task<SolicitudOperationResult> DeleteAsync(int idSolicitud, int idUsuarioEjecutor)
        {
            try
            {
                var existente = await _solicitudRepository.GetByIdAsync(idSolicitud);
                if (existente == null)
                {
                    return SolicitudOperationResult.Failure("La solicitud no existe.");
                }

                if (await _solicitudRepository.TieneRegistrosRelacionadosAsync(idSolicitud))
                {
                    return SolicitudOperationResult.Failure(MensajeNoEliminar);
                }

                await _solicitudRepository.DeleteAsync(idSolicitud);

                await _auditoriaService.RegistrarEliminacionAsync(idUsuarioEjecutor, Modulo, ToBitacoraPayload(existente));

                return SolicitudOperationResult.Success();
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                return SolicitudOperationResult.Failure("Ocurrio un error al eliminar la solicitud.");
            }
        }

        private Task RegistrarErrorAsync(int idUsuarioEjecutor, Exception exception)
            => _auditoriaService.RegistrarErrorSeguroAsync(idUsuarioEjecutor, Modulo, exception);

        private async Task<EstadoSolicitud> ObtenerEstadoPorNombreAsync(EstadoSolicitudNombre nombre)
        {
            var estado = await _estadoSolicitudRepository.GetByIdAsync((int)nombre);
            if (estado == null)
            {
                throw new InvalidOperationException(
                    $"No se encontro el estado '{nombre}' en EstadoSolicitud.");
            }
            return estado;
        }

        private static object ToBitacoraPayload(Solicitud s) => SolicitudBitacoraPayload.Crear(s);
    }
}
