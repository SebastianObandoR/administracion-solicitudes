using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class VencimientoService : IVencimientoService
    {
        private const string Modulo = "Solicitudes";
        private const int DiasHabilesVencimiento = 5;

        // El "usuario" del worker equivale a null
        private const int SinUsuario = 0;

        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IEstadoSolicitudRepository _estadoSolicitudRepository;
        private readonly IDiaFeriadoRepository _diaFeriadoRepository;
        private readonly IAuditoriaService _auditoriaService;
        private readonly INotificacionEstadoService _notificacionEstadoService;

        public VencimientoService(
            ISolicitudRepository solicitudRepository,
            IEstadoSolicitudRepository estadoSolicitudRepository,
            IDiaFeriadoRepository diaFeriadoRepository,
            IAuditoriaService auditoriaService,
            INotificacionEstadoService notificacionEstadoService)
        {
            _solicitudRepository = solicitudRepository;
            _estadoSolicitudRepository = estadoSolicitudRepository;
            _diaFeriadoRepository = diaFeriadoRepository;
            _auditoriaService = auditoriaService;
            _notificacionEstadoService = notificacionEstadoService;
        }

        public async Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor)
        {
            try
            {
                // Feriados en una sola consulta
                var feriados = await ObtenerFeriadosAsync();
                DateTime hoy = DateTime.Today;

                return solicitudes.ToDictionary(
                    s => s.IdSolicitud,
                    s => Evaluar(s, feriados, hoy));
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorSeguroAsync(idUsuarioEjecutor, Modulo, exception);
                throw;
            }
        }

        public async Task<int> ProcesarVencimientosAsync()
        {
            try
            {
                var candidatas = await _solicitudRepository.GetVencibleCandidatesAsync(
                    (int)EstadoSolicitudNombre.Vencida,
                    (int)EstadoSolicitudNombre.Finalizada);

                var feriados = await ObtenerFeriadosAsync();
                DateTime hoy = DateTime.Today;
           
                var vencidas = candidatas
                    .Where(s => Evaluar(s, feriados, hoy) == EstadoVencimiento.Vencida)
                    .ToList();

                if (vencidas.Count == 0) return 0;

                var estadoVencida = await ObtenerEstadoVencidaAsync();

                int marcadas = 0;
                foreach (var solicitud in vencidas)
                {
                    if (await MarcarComoVencidaAsync(solicitud, estadoVencida.IdEstado)) marcadas++;
                }

                return marcadas;
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorSeguroAsync(SinUsuario, Modulo, exception);
                throw;
            }
        }

        private async Task<bool> MarcarComoVencidaAsync(Solicitud solicitud, int idEstadoVencida)
        {
            try
            {
                var registroAnterior = SolicitudBitacoraPayload.Crear(solicitud);
                int idEstadoAnterior = solicitud.IdEstado;

                bool actualizada = await _solicitudRepository.MarcarComoVencidaAsync(
                    solicitud.IdSolicitud, solicitud.IdEstado, idEstadoVencida);

                if (!actualizada) return false;

                solicitud.IdEstado = idEstadoVencida;
                await _auditoriaService.RegistrarActualizacionAsync(
                    null, Modulo, registroAnterior, SolicitudBitacoraPayload.Crear(solicitud));

                await _notificacionEstadoService.NotificarCambioEstadoAsync(
                    solicitud.IdSolicitud, idEstadoAnterior, idEstadoVencida, SinUsuario, "Proceso automatico");

                return true;
            }
            catch (Exception exception)
            {          
                await _auditoriaService.RegistrarErrorSeguroAsync(SinUsuario, Modulo, exception);
                return false;
            }
        }

        private async Task<EstadoSolicitud> ObtenerEstadoVencidaAsync()
        {
            var estado = await _estadoSolicitudRepository.GetByIdAsync((int)EstadoSolicitudNombre.Vencida);
            if (estado == null)
            {
                throw new InvalidOperationException(
                    $"No se encontro el estado '{EstadoSolicitudNombre.Vencida}' en EstadoSolicitud.");
            }
            return estado;
        }

        private async Task<HashSet<DateTime>> ObtenerFeriadosAsync()
        {
            return (await _diaFeriadoRepository.GetFechasAsync())
                .Select(f => f.Date)
                .ToHashSet();
        }

        private static EstadoVencimiento Evaluar(Solicitud solicitud, HashSet<DateTime> feriados, DateTime hoy)
        {
            if (solicitud.IdEstado == (int)EstadoSolicitudNombre.Vencida)
            {
                return EstadoVencimiento.Vencida;
            }

            // Con fecha de respuesta ya no corre el plazo
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

        private static DateTime CalcularFechaLimite(DateTime fechaIngreso, HashSet<DateTime> feriados)
            => SumarDiasHabiles(fechaIngreso.Date, DiasHabilesVencimiento, feriados);

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
    }
}
