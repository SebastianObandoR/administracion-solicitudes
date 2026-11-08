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
        SolicitudPublicadaDetalle ObtenerDetalleSolicitudPublicada(int idSolicitud);

        [OperationContract]
        ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas);

        [OperationContract]
        bool RegistrarErrorImportacion(string referencia, string mensaje);

        [OperationContract]
        ResultadoConsultaParticipaciones ConsultarParticipaciones(string cedulaJuridica);

        [OperationContract]
        bool RegistrarErrorConsultaParticipaciones(string referencia, string mensaje);

        [OperationContract]
        ResultadoParticipacion RegistrarParticipacion(ParticipacionProveedor participacion);
    }
}
