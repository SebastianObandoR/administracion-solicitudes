using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public class ErrorLog
    {
        public int IdError { get; set; }

        public int? IdUsuario { get; set; }

        public string Modulo { get; set; } = string.Empty;

        public string MensajeError { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
    }
}
