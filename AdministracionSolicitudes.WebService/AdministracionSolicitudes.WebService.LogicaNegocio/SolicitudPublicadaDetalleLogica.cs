using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class SolicitudPublicadaDetalleLogica
    {
        private readonly SolicitudPublicadaDetalleAccesoDatos _accesoDatos;

        public SolicitudPublicadaDetalleLogica()
        {
            _accesoDatos = new SolicitudPublicadaDetalleAccesoDatos();
        }

        public SolicitudPublicadaDetalle ObtenerDetalleSolicitudPublicada(int idSolicitud)
        {
            return _accesoDatos.ObtenerDetalleSolicitudPublicada(idSolicitud);
        }
    }
}
