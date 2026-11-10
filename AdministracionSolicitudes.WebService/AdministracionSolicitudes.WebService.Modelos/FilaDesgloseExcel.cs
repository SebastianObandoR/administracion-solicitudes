using System.Runtime.Serialization;

namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class FilaDesgloseExcel
    {
        [DataMember]
        public int NumeroFila { get; set; }

        [DataMember]
        public string Mes { get; set; }

        [DataMember]
        public string Anio { get; set; }

        [DataMember]
        public string Horas { get; set; }

        [DataMember]
        public string Monto { get; set; }

        [DataMember]
        public string Iva { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public string Observaciones { get; set; }

        [DataMember]
        public string PorcentajeCobro { get; set; }
    }
}
