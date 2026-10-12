using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IRepresentanteService
    {
        Task<RepresentantePagina> ObtenerPaginaAsync(int pagina, int usuarioId);
        Task<Representante?> ObtenerPorIdAsync(int id, int usuarioId);
        Task<bool> CrearAsync(Representante representante, int usuarioId);
        Task<ActualizarRepresentanteResultado> ActualizarAsync(Representante representante, int usuarioId);
        Task<EliminarRepresentanteResultado> EliminarAsync(int id, int usuarioId);
    }
}
