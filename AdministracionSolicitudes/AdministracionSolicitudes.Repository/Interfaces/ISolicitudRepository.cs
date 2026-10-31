using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface ISolicitudRepository
    {
        Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize);
        Task<Solicitud?> GetByIdAsync(int idSolicitud);
        Task<Solicitud?> GetPublicadaVigenteByIdAsync(int idSolicitud);
        Task<bool> ExistsConsecutivoAsync(string consecutivoOficio, int? excludeId = null);
        Task<int> CreateAsync(Solicitud solicitud);
        Task UpdateAsync(Solicitud solicitud);
        Task DeleteAsync(int idSolicitud);
        Task<bool> TieneRegistrosRelacionadosAsync(int idSolicitud);

        // Usado por SOL5 (proceso automatico de vencimiento)
        Task<IEnumerable<Solicitud>> GetVencibleCandidatesAsync(int idEstadoVencida, int idEstadoFinalizada);
        Task<bool> MarcarComoVencidaAsync(int idSolicitud, int idEstadoAnterior, int idEstadoVencida);
    }
}
