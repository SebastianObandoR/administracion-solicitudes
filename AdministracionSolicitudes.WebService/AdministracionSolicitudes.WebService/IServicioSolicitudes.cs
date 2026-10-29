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
    }
}