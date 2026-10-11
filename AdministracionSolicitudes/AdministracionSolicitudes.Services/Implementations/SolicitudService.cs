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
        private const int DiasHabilesVencimiento = 5;

        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IEstadoSolicitudRepository _estadoSolicitudRepository;
        private readonly IDiaFeriadoRepository _diaFeriadoRepository;
        private readonly IAuditoriaService _auditoriaService;

        public SolicitudService(
            ISolicitudRepository solicitudRepository,
            IEstadoSolicitudRepository estadoSolicitudRepository,
            IDiaFeriadoRepository diaFeriadoRepository,
            IAuditoriaService auditoriaService)
        {
            _solicitudRepository = solicitudRepository;
            _estadoSolicitudRepository = estadoSolicitudRepository;
            _diaFeriadoRepository = diaFeriadoRepository;
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

        public async Task<SolicitudOperationResult> CreateAsync(Solicitud solicitud, int idUsuarioEjecutor)
        {
            try
            {
                if (await _solicitudRepository.ExistsConsecutivoAsync(solicitud.ConsecutivoOficio))
                {
                    return SolicitudOperationResult.Failure(MensajeConsecutivoDuplicado);
                }

                // Por defecto: nueva
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

        public async Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosVencimientoAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor)
        {
            try
            {
                // 1 sola lectura
                var feriados = (await _diaFeriadoRepository.GetFechasAsync())
                    .Select(f => f.Date)
                    .ToHashSet();

                DateTime hoy = DateTime.Today;

                return solicitudes.ToDictionary(
                    s => s.IdSolicitud,
                    s => EvaluarVencimiento(s, feriados, hoy));
            }
            catch (Exception exception)
            {
                await RegistrarErrorAsync(idUsuarioEjecutor, exception);
                throw;
            }
        }

        public static EstadoVencimiento EvaluarVencimiento(Solicitud solicitud, HashSet<DateTime> feriados, DateTime hoy)
        {
            if (solicitud.IdEstado == (int)EstadoSolicitudNombre.Vencida)
            {
                return EstadoVencimiento.Vencida;
            }

            // Con fecha de respuesta: no se avisa
            if (solicitud.FechaRespuesta.HasValue || solicitud.IdEstado == (int)EstadoSolicitudNombre.Finalizada)
            {
                return EstadoVencimiento.SinAlerta;
            }

            DateTime fechaLimite = CalcularFechaLimite(solicitud.FechaIngreso, feriados);

            if (hoy.Date > fechaLimite) return EstadoVencimiento.Vencida;
            if (hoy.Date == fechaLimite) return EstadoVencimiento.VenceHoy;

            return solicitud.IdEstado == (int)EstadoSolicitudNombre.Nueva
                ? EstadoVencimiento.Nueva
                : EstadoVencimiento.SinAlerta;
        }

        public static DateTime CalcularFechaLimite(DateTime fechaIngreso, HashSet<DateTime> feriados)
            => SumarDiasHabiles(fechaIngreso.Date, DiasHabilesVencimiento, feriados);

        private async Task RegistrarErrorAsync(int idUsuarioEjecutor, Exception exception)
        {
            try
            {
                int? idUsuario = idUsuarioEjecutor > 0 ? idUsuarioEjecutor : null;
                await _auditoriaService.RegistrarErrorAsync(idUsuario, Modulo, exception);
            }
            catch
            {

            }
        }

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

        // Excluye sabados, domingos y dias en DiaFeriado
        private static DateTime SumarDiasHabiles(DateTime fechaInicio, int diasHabiles, HashSet<DateTime> feriados)
        {
            DateTime fecha = fechaInicio;
            int sumados = 0;

            while (sumados < diasHabiles)
            {
                fecha = fecha.AddDays(1);
                bool esFinDeSemana = fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday;
                bool esFeriado = feriados.Contains(fecha.Date);

                if (!esFinDeSemana && !esFeriado)
                {
                    sumados++;
                }
            }

            return fecha.Date;
        }

        private static object ToBitacoraPayload(Solicitud s) => new
        {
            s.IdSolicitud,
            s.ConsecutivoOficio,
            s.Titulo,
            s.Descripcion,
            s.Observaciones,
            s.DocumentoInicio,
            s.DocumentoRespuesta,
            s.IdRepresentante,
            s.IdEstado,
            s.FechaIngreso,
            s.FechaRespuesta,
            s.FechaInicio
        };
    }
}
