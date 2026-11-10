using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public interface IConsultaParticipacionesServicio
    {
        ResultadoConsultaParticipaciones Consultar(string cedulaJuridica);

        bool RegistrarErrorConsulta(string referencia, string mensaje);
    }
}
