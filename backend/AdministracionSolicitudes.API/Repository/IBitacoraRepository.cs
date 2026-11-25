
namespace AdministracionSolicitudes.API.Repository
{
    public interface IBitacoraRepository
    {
        Task RegistrarAsync(
            int idUsuario,
            string modulo,
            string accion,
            string descripcion);

        Task RegistrarErrorAsync(
            int idUsuario,
            string modulo,
            string mensaje);
    }
}
