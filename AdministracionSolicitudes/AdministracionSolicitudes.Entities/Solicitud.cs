namespace AdministracionSolicitudes.Entities
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string ConsecutivoOficio { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Observaciones { get; set; }
        public string? DocumentoInicio { get; set; }
        public string? DocumentoRespuesta { get; set; }
        public int IdRepresentante { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public DateTime? FechaInicio { get; set; }

        public string? NombreRepresentante { get; set; }
        public string? NombreEstado { get; set; }
    }
}
