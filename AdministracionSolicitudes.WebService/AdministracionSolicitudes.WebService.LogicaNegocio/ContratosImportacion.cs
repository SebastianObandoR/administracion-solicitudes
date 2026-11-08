using System.Collections.Generic;
using AdministracionSolicitudes.WebService.Modelos;
namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public interface IImportacionPropuestaServicio
    {
        ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas);
        bool RegistrarErrorImportacion(string referencia, string mensaje);
    }
}
