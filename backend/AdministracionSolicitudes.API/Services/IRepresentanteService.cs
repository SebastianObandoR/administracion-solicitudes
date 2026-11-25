
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;

namespace AdministracionSolicitudes.API.Services
{
    public interface IRepresentanteService
    {
        Task<PagedResult<Representante>> ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            int usuarioId);

        Task<Representante?> ObtenerPorIdAsync(
            int id,
            int usuarioId);

        Task<bool> CrearAsync(
            Representante representante,
            int usuarioId);

        Task<ActualizarRepresentanteResultado> ActualizarAsync(
            Representante representante,
            int usuarioId);

        Task<EliminarRepresentanteResultado> EliminarAsync(
            int id,
            int usuarioId);
    }
}
