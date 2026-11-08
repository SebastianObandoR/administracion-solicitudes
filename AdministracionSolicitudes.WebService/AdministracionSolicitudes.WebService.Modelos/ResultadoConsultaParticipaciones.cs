using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class ResultadoConsultaParticipaciones
    {
        [DataMember]
        public bool EsCorrecto { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public List<SolicitudParticipacion> Solicitudes { get; set; }

        public ResultadoConsultaParticipaciones()
        {
            Mensaje = string.Empty;
            Solicitudes = new List<SolicitudParticipacion>();
        }
    }
}
