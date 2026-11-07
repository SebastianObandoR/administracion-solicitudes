using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class ResultadoParticipacion
    {
        [DataMember(Order = 1)] public bool Exitoso { get; set; }
        [DataMember(Order = 2)] public string Mensaje { get; set; }
        [DataMember(Order = 3)] public bool RequiereDatosProveedor { get; set; }
        [DataMember(Order = 4)] public int IdParticipacion { get; set; }
    }
}
