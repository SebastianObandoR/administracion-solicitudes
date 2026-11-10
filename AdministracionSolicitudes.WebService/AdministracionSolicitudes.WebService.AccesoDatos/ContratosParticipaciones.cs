using System.Collections.Generic;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public interface IParticipacionesRepositorio
    {
        bool ExisteProveedor(string cedulaJuridica);

        List<SolicitudParticipacion> ObtenerSolicitudes(string cedulaJuridica);
    }

    public interface ISeguimientoParticipacionesRepositorio
    {
        void RegistrarConsulta();

        void RegistrarError(string mensaje);
    }
}
