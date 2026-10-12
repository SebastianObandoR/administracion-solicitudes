using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class TareasListado
    {
        public Solicitud Solicitud { get; set; } = new();
        public PagedResult<Tarea> Tareas { get; set; } = new();
    }
}
