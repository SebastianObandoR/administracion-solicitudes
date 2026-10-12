using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public enum ResultadoEliminacion
    {
        Eliminado = 1,
        NoEncontrado = 2,
        TieneRelacionados = 3
    }
}
