using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface ITareaService
    {
        Task<ServiceResult<IReadOnlyList<SolicitudResumen>>> ObtenerSolicitudesAsync(int idUsuarioSesion);   // NUEVA
        Task<ServiceResult<TareasListado>> ObtenerTareasPorSolicitudAsync(int idSolicitud, int pagina, int idUsuarioSesion);
        Task<ServiceResult<TareaFormulario>> ObtenerFormularioAsync(int idSolicitud, int? idTarea, int idUsuarioSesion);
        Task<ServiceResult> CrearAsync(Tarea tarea, int idUsuarioSesion);
        Task<ServiceResult> ActualizarAsync(Tarea tarea, int idUsuarioSesion);
        Task<ServiceResult> EliminarAsync(int idTarea, int idUsuarioSesion);
    }
}