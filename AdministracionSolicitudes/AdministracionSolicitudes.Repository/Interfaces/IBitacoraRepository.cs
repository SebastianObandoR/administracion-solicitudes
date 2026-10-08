namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IBitacoraRepository
    {
        Task InsertAsync(int? idUsuario, string modulo, string accion, string descripcion, DateTime fecha);
    }
}
