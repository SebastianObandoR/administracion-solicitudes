using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class SolicitudPublicada
    {
        [DataMember(Order = 1)]
        public int IdSolicitud { get; set; }

        [DataMember(Order = 2)]
        public string ConsecutivoOficio { get; set; }

        [DataMember(Order = 3)]
        public string Titulo { get; set; }

        [DataMember(Order = 4)]
        public string NombreRepresentante { get; set; }
    }
}