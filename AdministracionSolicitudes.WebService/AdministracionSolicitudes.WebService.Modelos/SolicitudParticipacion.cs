using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class SolicitudParticipacion
    {
        [DataMember]
        public string ConsecutivoOficio { get; set; }

        [DataMember]
        public string Titulo { get; set; }
    }
}
