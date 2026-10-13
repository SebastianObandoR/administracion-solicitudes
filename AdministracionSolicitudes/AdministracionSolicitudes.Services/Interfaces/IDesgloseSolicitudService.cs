using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IDesgloseSolicitudService
    {
        Task<(IEnumerable<DesgloseSolicitud> Items, int TotalRegistros)> GetPagedAsync(int idSolicitud, int pageNumber, int pageSize, int idUsuario);
        Task<DesgloseSolicitud?> GetByIdAsync(int idSolicitud, int mes, int anio, int idUsuario);
        Task<ResultadoOperacion> CreateAsync(DesgloseSolicitud desglose, int idUsuario);
        Task<ResultadoOperacion> UpdateAsync(DesgloseSolicitud desglose, int originalMes, int originalAnio, int idUsuario);
        Task<ResultadoOperacion> DeleteAsync(int idSolicitud, int mes, int anio, int idUsuario);
    }
}
