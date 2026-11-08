using System.Configuration;
using AdministracionSolicitudes.WebService.LogicaNegocio;
using AdministracionSolicitudes.WebService.Modelos;
using System.Collections.Generic;

namespace AdministracionSolicitudes.WebService
{
    public class ServicioSolicitudes : IServicioSolicitudes
    {
        private readonly IImportacionPropuestaServicio _importacion;

        public ServicioSolicitudes() : this(CrearImportacion()) {}

        internal ServicioSolicitudes(IImportacionPropuestaServicio importacion)
        {
            _importacion = importacion;
        }

        private static IImportacionPropuestaServicio CrearImportacion()
        {
            var configuracion = ConfigurationManager.ConnectionStrings["PublicaSA"];
            if (configuracion == null) throw new ConfigurationErrorsException("No se encontró la conexión PublicaSA.");
            return new ImportacionPropuestaServicio(configuracion.ConnectionString);
        }

        public ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas)
        {
            return _importacion.CargarDesglosesPropuesta(filas);
        }

        public bool RegistrarErrorImportacion(string referencia, string mensaje)
        {
            return _importacion.RegistrarErrorImportacion(referencia, mensaje);
        }

        public List<SolicitudPublicada> ObtenerSolicitudesPublicadas()
        {
            var logica = new SolicitudPublicadaLogica();

            return logica.ObtenerSolicitudesPublicadas();
        }
    }
}
