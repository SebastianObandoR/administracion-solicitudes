using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Entities
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public int IdSolicitud { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal HorasInvertidas { get; set; }
    }
}
