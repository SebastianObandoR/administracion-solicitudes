using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;
using System.Collections.Generic;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class SolicitudPublicadaLogica
    {
        private readonly SolicitudPublicadaAccesoDatos _accesoDatos;

        public SolicitudPublicadaLogica()
        {
            _accesoDatos = new SolicitudPublicadaAccesoDatos();
        }

        public List<SolicitudPublicada> ObtenerSolicitudesPublicadas()
        {
            return _accesoDatos.ObtenerSolicitudesPublicadas();
        }
    }
}