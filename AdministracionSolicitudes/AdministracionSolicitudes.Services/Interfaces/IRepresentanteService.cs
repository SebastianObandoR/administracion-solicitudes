using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IRepresentanteService
    {
        Task<RepresentantePagina> ObtenerPaginaAsync(int pagina, int usuarioId, int? idRepresentante = null);
        Task<Representante?> ObtenerPorIdAsync(int id, int usuarioId);
        Task<(Representante? Representante, Dictionary<string, string[]> Errores)> GuardarAsync(
            RepresentanteInputModel input, int usuarioId, int? id = null);
        Task<EliminarRepresentanteResultado> EliminarAsync(int id, int usuarioId);
    }
}
