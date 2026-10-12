namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IDiaFeriadoRepository
    {
        Task<IEnumerable<DateTime>> GetFechasAsync();
    }
}
