using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IEstadoSolicitudRepository
    {
        Task<IEnumerable<EstadoSolicitud>> GetAllAsync();
        Task<EstadoSolicitud?> GetByIdAsync(int idEstado);
        Task<(IReadOnlyList<EstadoSolicitud> Estados, int Total)> ObtenerPaginaAsync(int pagina, int tamanoPagina, int usuarioId);
        Task<EstadoSolicitud?> ObtenerPorIdAsync(int id, int usuarioId);
        Task<bool> CrearAsync(EstadoSolicitud estado, int usuarioId);
        Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(EstadoSolicitud estado, int usuarioId);
        Task<EliminarEstadoSolicitudResultado> EliminarAsync(int id, int usuarioId);
        Task RegistrarErrorAsync(int usuarioId, string mensaje);
    }

    public enum ActualizarEstadoSolicitudResultado
    {
        Actualizado,
        NoEncontrado,
        NombreDuplicado
    }

    public enum EliminarEstadoSolicitudResultado
    {
        Eliminado,
        NoEncontrado,
        TieneSolicitudes
    }
}