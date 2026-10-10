using System;
using System.Collections.Generic;
using System.Text;

using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class TareaFormulario
    {
        public SolicitudResumen Solicitud { get; set; } = new();
        public Tarea? Tarea { get; set; }
        public IReadOnlyList<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}