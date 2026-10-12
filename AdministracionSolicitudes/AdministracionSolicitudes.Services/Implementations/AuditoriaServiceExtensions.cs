using AdministracionSolicitudes.Services.Interfaces;

namespace AdministracionSolicitudes.Services.Implementations
{
    internal static class AuditoriaServiceExtensions
    {
        public static async Task RegistrarErrorSeguroAsync(this IAuditoriaService auditoriaService, int idUsuarioEjecutor, string modulo, Exception exception)
        {
            try
            {
                int? idUsuario = idUsuarioEjecutor > 0 ? idUsuarioEjecutor : null;
                await auditoriaService.RegistrarErrorAsync(idUsuario, modulo, exception);
            }
            catch
            {

            }
        }
    }
}
