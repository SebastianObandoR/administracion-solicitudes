using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    // // SIMULACION SOL4: repositorio minimo de solo lectura (combo de estados). Reemplazar por el de SOL4 al integrar
    public interface IEstadoSolicitudRepository
    {
        Task<IEnumerable<EstadoSolicitud>> GetAllAsync();
        Task<EstadoSolicitud?> GetByIdAsync(int idEstado);
    }
}
