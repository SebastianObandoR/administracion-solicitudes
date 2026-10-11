namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IErrorLogRepository
    {
        Task RegistrarAsync(int usuarioId, string modulo, string mensaje);
    }
}
