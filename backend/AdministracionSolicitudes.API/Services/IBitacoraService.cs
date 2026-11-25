
namespace AdministracionSolicitudes.API.Services
{
    public interface IBitacoraService
    {
        Task RegistrarConsultaAsync(
            int idUsuario,
            string modulo,
            string elemento);

        Task RegistrarCreacionAsync(
            int idUsuario,
            string modulo,
            object registro);

        Task RegistrarActualizacionAsync(
            int idUsuario,
            string modulo,
            object anterior,
            object actual);

        Task RegistrarEliminacionAsync(
            int idUsuario,
            string modulo,
            object registro);

        Task RegistrarAccionAsync(
            int idUsuario,
            string modulo,
            string accion,
            string descripcion);

        Task RegistrarErrorAsync(
            int idUsuario,
            string modulo,
            Exception exception);
    }
}
