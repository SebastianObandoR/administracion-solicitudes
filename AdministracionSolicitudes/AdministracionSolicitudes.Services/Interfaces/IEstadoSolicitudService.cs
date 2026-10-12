using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IEstadoSolicitudService
    {
        Task<EstadoSolicitudPagina> ObtenerPaginaAsync(int pagina, int usuarioId);
        Task<EstadoSolicitud?> ObtenerPorIdAsync(int id, int usuarioId);
        Task<bool> CrearAsync(EstadoSolicitud estado, int usuarioId);
        Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(EstadoSolicitud estado, int usuarioId);
        Task<EliminarEstadoSolicitudResultado> EliminarAsync(int id, int usuarioId);
    }
}
