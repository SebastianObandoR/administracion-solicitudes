using AdministracionSolicitudes.WebService.Modelos;
using System.Collections.Generic;
using System.ServiceModel;

namespace AdministracionSolicitudes.WebService
{
    [ServiceContract]
    public interface IServicioSolicitudes
    {
        [OperationContract]
        List<SolicitudPublicada> ObtenerSolicitudesPublicadas();
        [OperationContract]
        ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas);

        [OperationContract]
        bool RegistrarErrorImportacion(string referencia, string mensaje);
    }
}
