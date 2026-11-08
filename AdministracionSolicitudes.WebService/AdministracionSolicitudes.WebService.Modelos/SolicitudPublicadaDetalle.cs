using System;
using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class SolicitudPublicadaDetalle
    {
        [DataMember(Order = 1)]
        public int IdSolicitud { get; set; }

        [DataMember(Order = 2)]
        public string ConsecutivoOficio { get; set; }

        [DataMember(Order = 3)]
        public string DocumentoRespuesta { get; set; }

        [DataMember(Order = 4)]
        public string DocumentoInicio { get; set; }

        [DataMember(Order = 5)]
        public string Titulo { get; set; }

        [DataMember(Order = 6)]
        public string Descripcion { get; set; }

        [DataMember(Order = 7)]
        public string NombreRepresentante { get; set; }

        [DataMember(Order = 8)]
        public string Observaciones { get; set; }

        [DataMember(Order = 9)]
        public string NombreEstado { get; set; }

        [DataMember(Order = 10)]
        public DateTime FechaIngreso { get; set; }

        [DataMember(Order = 11)]
        public DateTime? FechaRespuesta { get; set; }

        [DataMember(Order = 12)]
        public DateTime? FechaInicio { get; set; }

        [DataMember(Order = 13)]
        public DateTime? FechaVencimientoPublicacion { get; set; }

        [DataMember(Order = 14)]
        public bool Publicada { get; set; }
    }
}
