
using AdministracionSolicitudes.API.Entities;

namespace AdministracionSolicitudes.API.Repository
{
    public interface IRepresentanteRepository
    {
        Task<IEnumerable<Representante>> GetAllAsync();

        Task<(IReadOnlyList<Representante> Representantes, int Total)>
            ObtenerPaginaAsync(
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

    public enum ActualizarRepresentanteResultado
    {
        Actualizado,
        NoEncontrado,
        CorreoDuplicado
    }

    public enum EliminarRepresentanteResultado
    {
        Eliminado,
        NoEncontrado,
        TieneSolicitudes
    }
}
