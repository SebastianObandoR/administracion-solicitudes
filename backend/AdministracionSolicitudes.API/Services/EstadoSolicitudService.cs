
using System.Data.Common;
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;

namespace AdministracionSolicitudes.API.Services
{
    public class EstadoSolicitudService : IEstadoSolicitudService
    {
        private readonly IEstadoSolicitudRepository _estadoRepository;

        public EstadoSolicitudService(
            IEstadoSolicitudRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<PagedResult<EstadoSolicitud>> ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            int usuarioId)
        {
            return await EjecutarAsync(async () =>
            {
                var resultado = await _estadoRepository.ObtenerPaginaAsync(
                    pagina,
                    tamanoPagina,
                    usuarioId
                );

                return new PagedResult<EstadoSolicitud>
                {
                    Items = resultado.Estados,
                    PageNumber = pagina,
                    PageSize = tamanoPagina,
                    TotalCount = resultado.Total
                };
            }, usuarioId);
        }

        public Task<EstadoSolicitud?> ObtenerPorIdAsync(
            int id,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _estadoRepository.ObtenerPorIdAsync(id, usuarioId),
                usuarioId
            );
        }

        public Task<bool> CrearAsync(
            EstadoSolicitud estado,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _estadoRepository.CrearAsync(estado, usuarioId),
                usuarioId
            );
        }

        public Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(
            EstadoSolicitud estado,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _estadoRepository.ActualizarAsync(estado, usuarioId),
                usuarioId
            );
        }

        public Task<EliminarEstadoSolicitudResultado> EliminarAsync(
            int id,
            int usuarioId)
        {
            return EjecutarAsync(
                () => _estadoRepository.EliminarAsync(id, usuarioId),
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
                    await _estadoRepository.RegistrarErrorAsync(
                        usuarioId,
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
