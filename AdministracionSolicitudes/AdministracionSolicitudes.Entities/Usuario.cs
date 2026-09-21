namespace AdministracionSolicitudes.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaCifrada { get; set; } = string.Empty;
        public string NonceContrasena { get; set; } = string.Empty;
        public string TagContrasena { get; set; } = string.Empty;
        public EstadoUsuario Estado { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}