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
        private readonly IBitacoraService _bitacoraService;

        public SolicitudService(
            ISolicitudRepository solicitudRepository,
            IEstadoSolicitudRepository estadoSolicitudRepository,
            IDiaFeriadoRepository diaFeriadoRepository,
            IBitacoraService bitacoraService)
        {
            _solicitudRepository = solicitudRepository;
            _estadoSolicitudRepository = estadoSolicitudRepository;
            _diaFeriadoRepository = diaFeriadoRepository;
            _bitacoraService = bitacoraService;
        }

        public async Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize, int? idUsuarioActual)
        {
            var result = await _solicitudRepository.GetPagedAsync(pageNumber, pageSize);
            await _bitacoraService.RegistrarConsultaAsync(idUsuarioActual, Modulo, "Solicitudes");
            return result;
        }

        public async Task<Solicitud?> GetByIdAsync(int idSolicitud, int? idUsuarioActual)
        {
            var solicitud = await _solicitudRepository.GetByIdAsync(idSolicitud);
            if (solicitud != null)
            {
                await _bitacoraService.RegistrarConsultaAsync(idUsuarioActual, Modulo, $"Solicitud #{idSolicitud}");
            }
            return solicitud;
        }

        public async Task<SolicitudOperationResult> CreateAsync(Solicitud solicitud, int? idUsuarioActual)
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

            await _bitacoraService.RegistrarCreacionAsync(idUsuarioActual, Modulo, ToBitacoraPayload(solicitud));

            return SolicitudOperationResult.Success(idSolicitud);
        }

        public async Task<SolicitudOperationResult> UpdateAsync(Solicitud solicitud, int? idUsuarioActual)
        {
            var existente = await _solicitudRepository.GetByIdAsync(solicitud.IdSolicitud);
            if (existente == null)
            {
                return SolicitudOperationResult.Failure("La solicitud no existe.");
            }

            if (await _solicitudRepository.ExistsConsecutivoAsync(solicitud.ConsecutivoOficio, solicitud.IdSolicitud))
            {
                return SolicitudOperationResult.Failure(MensajeConsecutivoDuplicado);
            }

            var registroAnterior = ToBitacoraPayload(existente);

            await _solicitudRepository.UpdateAsync(solicitud);

            var actualizado = await _solicitudRepository.GetByIdAsync(solicitud.IdSolicitud);
            var registroActual = ToBitacoraPayload(actualizado!);

            await _bitacoraService.RegistrarActualizacionAsync(idUsuarioActual, Modulo, registroAnterior, registroActual);

            return SolicitudOperationResult.Success(solicitud.IdSolicitud);
        }

        public async Task<SolicitudOperationResult> DeleteAsync(int idSolicitud, int? idUsuarioActual)
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

            await _bitacoraService.RegistrarEliminacionAsync(idUsuarioActual, Modulo, ToBitacoraPayload(existente));

            return SolicitudOperationResult.Success();
        }

        public async Task<EstadoVencimiento> CalcularEstadoVencimientoAsync(Solicitud solicitud)
        {
            // Si ya tiene respuesta, no se considera vencida ni pendiente
            if (solicitud.FechaRespuesta.HasValue)
            {
                return EstadoVencimiento.Nueva;
            }

            var feriados = (await _diaFeriadoRepository.GetFechasAsync())
                .Select(f => f.Date)
                .ToHashSet();

            DateTime fechaLimite = SumarDiasHabiles(solicitud.FechaIngreso, DiasHabilesVencimiento, feriados);
            DateTime hoy = DateTime.Today;

            if (hoy > fechaLimite) return EstadoVencimiento.Vencida;
            if (hoy == fechaLimite) return EstadoVencimiento.VenceHoy;
            return EstadoVencimiento.Nueva;
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
