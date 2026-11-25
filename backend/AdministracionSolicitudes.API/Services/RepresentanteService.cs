
using System.Data.Common;
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;

namespace AdministracionSolicitudes.API.Services
{
    public class RepresentanteService : IRepresentanteService
    {
        private const string Modulo = "Representantes";

        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IBitacoraRepository _bitacoraRepository;

        public RepresentanteService(
            IRepresentanteRepository representanteRepository,
            IBitacoraRepository bitacoraRepository)
        {
            _representanteRepository = representanteRepository;
            _bitacoraRepository = bitacoraRepository;
        }

        public async Task<PagedResult<Representante>> ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            int usuarioId)
        {
            return await EjecutarAsync(async () =>
            {
                var resultado = await _representanteRepository.ObtenerPaginaAsync(
                    pagina,
                    tamanoPagina,
                    usuarioId
                );

                return new PagedResult<Representante>
                {
                    Items = resultado.Representantes,
                    PageNumber = pagina,
                    PageSize = tamanoPagina,
                    TotalCount = resultado.Total
                };
            }, usuarioId);
        }

        public Task<Representante?> ObtenerPorIdAsync(
            int id,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _representanteRepository.ObtenerPorIdAsync(id, usuarioId),
                usuarioId
            );
        }

        public Task<bool> CrearAsync(
            Representante representante,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _representanteRepository.CrearAsync(representante, usuarioId),
                usuarioId
            );
        }

        public Task<ActualizarRepresentanteResultado> ActualizarAsync(
            Representante representante,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _representanteRepository.ActualizarAsync(representante, usuarioId),
                usuarioId
            );
        }

        public Task<EliminarRepresentanteResultado> EliminarAsync(
            int id,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _representanteRepository.EliminarAsync(id, usuarioId),
                usuarioId
            );
        }

        private async Task<T> EjecutarAsync<T>(
            Func<Task<T>> operacion,
            int usuarioId)
        {
            try
            {
                return await operacion();
            }
            catch (DbException exception)
            {
                try
                {
                    await _bitacoraRepository.RegistrarErrorAsync(
                        usuarioId,
                        Modulo,
                        exception.Message
                    );
                }
                catch (DbException errorLogException)
                {
                    throw new AggregateException(
                        "La operacion fallo y tambien fallo el registro del error tecnico.",
                        exception,
                        errorLogException
                    );
                }

                throw;
            }
        }
    }
}
