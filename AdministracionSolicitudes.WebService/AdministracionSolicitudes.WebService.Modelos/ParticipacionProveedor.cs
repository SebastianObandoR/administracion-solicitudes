using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class ParticipacionProveedor
    {
        [DataMember(Order = 1)] public int IdSolicitud { get; set; }
        [DataMember(Order = 2)] public string CedulaJuridica { get; set; }
        [DataMember(Order = 3)] public string NombreProveedor { get; set; }
        [DataMember(Order = 4)] public string NombreRepresentante { get; set; }
        [DataMember(Order = 5)] public string Telefono { get; set; }
        [DataMember(Order = 6)] public string Correo { get; set; }
        [DataMember(Order = 7)] public List<ParticipacionDesglose> Desgloses { get; set; }
    }
}
