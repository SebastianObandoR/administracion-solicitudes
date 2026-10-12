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

        private readonly IDiaFeriadoRepository _diaFeriadoRepository;
        private readonly IAuditoriaService _auditoriaService;

        public VencimientoService(
            IDiaFeriadoRepository diaFeriadoRepository,
            IAuditoriaService auditoriaService)
        {
            _diaFeriadoRepository = diaFeriadoRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<Dictionary<int, EstadoVencimiento>> CalcularEstadosAsync(IEnumerable<Solicitud> solicitudes, int idUsuarioEjecutor)
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
                    s => Evaluar(s, feriados, hoy));
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorSeguroAsync(idUsuarioEjecutor, Modulo, exception);
                throw;
            }
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
