using System.Collections.Generic;
using System.Runtime.Serialization;
namespace AdministracionSolicitudes.WebService.Modelos
{
    [DataContract]
    public class ResultadoCargaPropuesta
    {
        [DataMember] public bool EsCorrecto { get; set; }
        [DataMember] public List<string> Errores { get; set; }
        [DataMember] public List<FilaDesgloseExcel> Desgloses { get; set; }
        public ResultadoCargaPropuesta()
        {
            Errores = new List<string>();
            Desgloses = new List<FilaDesgloseExcel>();
        }
    }
}
