
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;

namespace AdministracionSolicitudes.API.Services
{
    public interface IEstadoSolicitudService
    {
        Task<PagedResult<EstadoSolicitud>> ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            int usuarioId);

        Task<EstadoSolicitud?> ObtenerPorIdAsync(
            int id,
            int usuarioId);

        Task<bool> CrearAsync(
            EstadoSolicitud estado,
            int usuarioId);

        Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(
            EstadoSolicitud estado,
            int usuarioId);

        Task<EliminarEstadoSolicitudResultado> EliminarAsync(
            int id,
            int usuarioId);
    }
}
