using System.Data.Common;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class EstadoSolicitudService : IEstadoSolicitudService
    {
        private const int TamanoPagina = 10;
        private readonly IEstadoSolicitudRepository _estadoRepository;

        public EstadoSolicitudService(IEstadoSolicitudRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<EstadoSolicitudPagina> ObtenerPaginaAsync(int pagina, int usuarioId)
        {
            pagina = Math.Max(pagina, 1);

            return await EjecutarAsync(async () =>
            {
                var resultado = await _estadoRepository.ObtenerPaginaAsync(pagina, TamanoPagina, usuarioId);
                return new EstadoSolicitudPagina
                {
                    Estados = resultado.Estados,
                    Pagina = pagina,
                    TamanoPagina = TamanoPagina,
                    Total = resultado.Total
                };
            }, usuarioId);
        }

        public Task<EstadoSolicitud?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _estadoRepository.ObtenerPorIdAsync(id, usuarioId), usuarioId);
        }

        public Task<bool> CrearAsync(EstadoSolicitud estado, int usuarioId)
        {
            return EjecutarAsync(() => _estadoRepository.CrearAsync(estado, usuarioId), usuarioId);
        }

        public Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(EstadoSolicitud estado, int usuarioId)
        {
            return EjecutarAsync(() => _estadoRepository.ActualizarAsync(estado, usuarioId), usuarioId);
        }

        public Task<EliminarEstadoSolicitudResultado> EliminarAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _estadoRepository.EliminarAsync(id, usuarioId), usuarioId);
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
                    await _estadoRepository.RegistrarErrorAsync(usuarioId, exception.Message);
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
