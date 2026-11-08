namespace AdministracionSolicitudes.Entities
{
    public class ParticipacionDetalle
    {
        public ParticipacionSolicitud Participacion { get; set; } = new();
        public IReadOnlyList<DesglosePropuesta> Desgloses { get; set; } = Array.Empty<DesglosePropuesta>();
        public bool SolicitudTieneAprobada { get; set; }
    }
}
