using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class ParticipacionDesglose
    {
        [DataMember(Order = 1)] public int Mes { get; set; }
        [DataMember(Order = 2)] public int Anio { get; set; }
        [DataMember(Order = 3)] public decimal Horas { get; set; }
        [DataMember(Order = 4)] public decimal Monto { get; set; }
        [DataMember(Order = 5)] public decimal Iva { get; set; }
        [DataMember(Order = 6)] public decimal Total { get; set; }
        [DataMember(Order = 7)] public string Observaciones { get; set; }
        [DataMember(Order = 8)] public decimal PorcentajeCobro { get; set; }
    }
}
