namespace AdministracionSolicitudes.Entities
{
    public class DatosNotificacionEstado
    {
        public int IdSolicitud { get; set; }
        public string ConsecutivoOficio { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string NombreRepresentante { get; set; } = string.Empty;
        public string CorreoRepresentante { get; set; } = string.Empty;
        public string EstadoAnterior { get; set; } = string.Empty;
        public string EstadoNuevo { get; set; } = string.Empty;
    }
}
