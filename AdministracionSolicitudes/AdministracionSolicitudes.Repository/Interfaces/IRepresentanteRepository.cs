using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    // // SIMULACION SOL3: repositorio minimo de solo lectura (combo de representantes). Reemplazar por el de SOL3 al integrar
    public interface IRepresentanteRepository
    {
        Task<IEnumerable<Representante>> GetAllAsync();
    }
}
