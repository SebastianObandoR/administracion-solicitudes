using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface ITareaRepository
    {
        Task<IEnumerable<Tarea>> ObtenerSolicitudPaginacionAsync(int idSolicitud, int pagina, int tamanoPagina);
        Task<int> ContarSolicitudAsync(int idSolicitud);
        Task<Tarea?> ObtenerIdAsync(int idTarea);
        Task<int> InsertarAsync(Tarea tarea);
        Task<bool> ActualizarAsync(Tarea tarea);
        Task<ResultadoEliminacion> EliminarAsync(int idTarea);
        Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();
        Task<SolicitudResumen?> ObtenerSolicitudAsync(int idSolicitud);
        Task<IEnumerable<SolicitudResumen>> ObtenerSolicitudesAsync();
    }
}