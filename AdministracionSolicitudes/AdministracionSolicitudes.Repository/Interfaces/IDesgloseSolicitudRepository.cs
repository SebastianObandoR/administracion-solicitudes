using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IDesgloseSolicitudRepository
    {
        Task<(IEnumerable<DesgloseSolicitud> Items, int TotalRegistros)> GetPagedAsync(int idSolicitud, int pageNumber, int pageSize);
        Task<DesgloseSolicitud?> GetByIdAsync(int idSolicitud, int mes, int anio);
        Task<bool> ExistsAsync(int idSolicitud, int mes, int anio, int? originalMes = null, int? originalAnio = null);
        Task CreateAsync(DesgloseSolicitud desglose);
        Task<bool> UpdateAsync(DesgloseSolicitud desglose, int originalMes, int originalAnio);
        Task<bool> DeleteAsync(int idSolicitud, int mes, int anio);
    }
}
