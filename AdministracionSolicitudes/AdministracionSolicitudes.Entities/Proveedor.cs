namespace AdministracionSolicitudes.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string CedulaJuridica { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string NombreRepresentante { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
