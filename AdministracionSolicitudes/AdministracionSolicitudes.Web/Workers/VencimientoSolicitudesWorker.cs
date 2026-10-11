using AdministracionSolicitudes.Services.Interfaces;

namespace AdministracionSolicitudes.Web.Workers
{
    public class VencimientoSolicitudesWorker : BackgroundService
    {
        private const string ClaveIntervalo = "Solicitudes:IntervaloVencimientoMinutos";
        private const int IntervaloPorDefectoMinutos = 60;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<VencimientoSolicitudesWorker> _logger;

        public VencimientoSolicitudesWorker(
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration,
            ILogger<VencimientoSolicitudesWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int minutos = _configuration.GetValue(ClaveIntervalo, IntervaloPorDefectoMinutos);
            if (minutos < 1) minutos = IntervaloPorDefectoMinutos;

            using var temporizador = new PeriodicTimer(TimeSpan.FromMinutes(minutos));

            try
            {
                do
                {
                    await EjecutarAsync();
                }
                while (await temporizador.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {
                
            }
        }

        private async Task EjecutarAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var vencimientoService = scope.ServiceProvider.GetRequiredService<IVencimientoService>();

                int marcadas = await vencimientoService.ProcesarVencimientosAsync();

                if (marcadas > 0)
                {
                    _logger.LogInformation("SOL5: {Cantidad} solicitud(es) marcada(s) como vencida(s).", marcadas);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "SOL5: fallo el proceso de vencimiento de solicitudes.");
            }
        }
    }
}
