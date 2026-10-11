using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }

        public int? IdUsuario { get; set; }

        public string Modulo { get; set; } = string.Empty;

        public string Accion { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
    }
}