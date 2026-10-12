using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public class TareaReporte
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal HorasInvertidas { get; set; }
        public string ConsecutivoOficio { get; set; } = string.Empty;
        public string TituloSolicitud { get; set; } = string.Empty;
    }
}