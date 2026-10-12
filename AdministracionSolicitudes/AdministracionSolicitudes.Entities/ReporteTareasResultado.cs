using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class ReporteTareasResultado
    {
        public IReadOnlyList<TareaReporte> Filas { get; set; } = new List<TareaReporte>();
        public decimal TotalHoras { get; set; }
    }
}
