using AdministracionSolicitudes.WebService.LogicaNegocio;
using AdministracionSolicitudes.WebService.Modelos;
using System.Collections.Generic;

namespace AdministracionSolicitudes.WebService
{
    public class ServicioSolicitudes : IServicioSolicitudes
    {
        public List<SolicitudPublicada> ObtenerSolicitudesPublicadas()
        {
            var logica = new SolicitudPublicadaLogica();

            return logica.ObtenerSolicitudesPublicadas();
        }

        public SolicitudPublicadaDetalle ObtenerDetalleSolicitudPublicada(int idSolicitud)
        {
            var logica = new SolicitudPublicadaDetalleLogica();

            return logica.ObtenerDetalleSolicitudPublicada(idSolicitud);
        }

        public ResultadoParticipacion RegistrarParticipacion(ParticipacionProveedor participacion)
        {
            var logica = new ParticipacionLogica();

            return logica.RegistrarParticipacion(participacion);
        }
    }
}