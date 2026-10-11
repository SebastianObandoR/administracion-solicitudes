using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IReporteTareasSolicitudRepository
    {
        Task<IEnumerable<TareaReporte>> ObtenerPorSolicitudMesAsync(int idSolicitud, DateTime desde, DateTime hasta);
        Task<IEnumerable<SolicitudResumen>> ObtenerSolicitudesAsync();
    }
}