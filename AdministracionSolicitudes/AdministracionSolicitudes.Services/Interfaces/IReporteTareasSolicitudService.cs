using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IReporteTareasSolicitudService
    {
        Task<ServiceResult<IReadOnlyList<Solicitud>>> ObtenerSolicitudesAsync(int idUsuarioSesion);
        Task<ServiceResult<ReporteTareasResultado>> GenerarReporteAsync(int idSolicitud, int mes, int anio, int idUsuarioSesion);
    }
}
