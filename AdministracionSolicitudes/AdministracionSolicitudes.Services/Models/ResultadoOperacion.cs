using System;
using System.Collections.Generic;
using System.Text;


namespace AdministracionSolicitudes.Services.Models
{
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;

        public static ResultadoOperacion Exito(string mensaje)
        {
            return new ResultadoOperacion { Exitoso = true, Mensaje = mensaje };
        }

        public static ResultadoOperacion Error(string mensaje)
        {
            return new ResultadoOperacion { Exitoso = false, Mensaje = mensaje };
        }
    }
}
