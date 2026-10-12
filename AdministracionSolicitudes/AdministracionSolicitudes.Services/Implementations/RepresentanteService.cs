using System.Data.Common;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class RepresentanteService : IRepresentanteService
    {
        private const int TamanoPagina = 10;
        private const string Modulo = "Representantes";
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        public RepresentanteService(
            IRepresentanteRepository representanteRepository,
            IErrorLogRepository errorLogRepository)
        {
            _representanteRepository = representanteRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<RepresentantePagina> ObtenerPaginaAsync(int pagina, int usuarioId)
        {
            pagina = Math.Max(pagina, 1);
            return await EjecutarAsync(async () =>
            {
                var resultado = await _representanteRepository.ObtenerPaginaAsync(pagina, TamanoPagina, usuarioId);
                return new RepresentantePagina
                {
                    Representantes = resultado.Representantes,
                    Pagina = pagina,
                    TamanoPagina = TamanoPagina,
                    Total = resultado.Total
                };
            }, usuarioId);
        }

        public Task<Representante?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.ObtenerPorIdAsync(id, usuarioId), usuarioId);
        }

        public Task<bool> CrearAsync(Representante representante, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.CrearAsync(representante, usuarioId), usuarioId);
        }

        public Task<ActualizarRepresentanteResultado> ActualizarAsync(Representante representante, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.ActualizarAsync(representante, usuarioId), usuarioId);
        }

        public Task<EliminarRepresentanteResultado> EliminarAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.EliminarAsync(id, usuarioId), usuarioId);
        }

        private async Task<T> EjecutarAsync<T>(Func<Task<T>> operacion, int usuarioId)
        {
            try
            {
                return await operacion();
            }
            catch (DbException exception)
            {
                try
                {
                    await _errorLogRepository.RegistrarAsync(new ErrorLog
                    {
                        IdUsuario = usuarioId,
                        Modulo = Modulo,
                        MensajeError = exception.Message
                    });
                }
                catch (DbException errorLogException)
                {
                    throw new AggregateException(
                        "La operación falló y también falló el registro del error técnico.",
                        exception,
                        errorLogException);
                }

                throw;
            }
        }
    }
}
