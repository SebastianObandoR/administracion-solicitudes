using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public class SolicitudResumen
    {
        public int IdSolicitud { get; set; }
        public string ConsecutivoOficio { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
