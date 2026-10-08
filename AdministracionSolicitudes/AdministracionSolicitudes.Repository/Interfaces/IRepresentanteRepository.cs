using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    // Repositorio minimo de solo lectura para SOL1 (combo de representantes)
    public interface IRepresentanteRepository
    {
        Task<IEnumerable<Representante>> GetAllAsync();
    }
}
