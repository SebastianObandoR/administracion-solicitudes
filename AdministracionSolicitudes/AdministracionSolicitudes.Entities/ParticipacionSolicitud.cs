namespace AdministracionSolicitudes.Entities
{
    public class ParticipacionSolicitud
    {
        public int IdParticipacion { get; set; }
        public int IdSolicitud { get; set; }
        public int IdProveedor { get; set; }
        public string CedulaJuridica { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public DateTime FechaParticipacion { get; set; }
        public bool Aprobada { get; set; }
    }
}
