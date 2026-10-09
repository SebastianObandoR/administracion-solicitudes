namespace AdministracionSolicitudes.Services.Interfaces
{
    // Nota: Simulacion temporal de Bitacora, luego se ajusta a la version que ya existe y se ajustan las llamadas en SolicitudService
    public interface IBitacoraService
    {
        Task RegistrarCreacionAsync(int? idUsuario, string modulo, object registroNuevo);
        Task RegistrarActualizacionAsync(int? idUsuario, string modulo, object registroAnterior, object registroActual);
        Task RegistrarEliminacionAsync(int? idUsuario, string modulo, object registroEliminado);
        Task RegistrarConsultaAsync(int? idUsuario, string modulo, string elemento);
    }
}
