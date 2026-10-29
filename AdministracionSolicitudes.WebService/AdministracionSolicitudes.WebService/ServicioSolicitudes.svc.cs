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
    }
}