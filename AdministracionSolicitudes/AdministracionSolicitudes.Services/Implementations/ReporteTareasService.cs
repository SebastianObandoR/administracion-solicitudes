using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class ReporteTareasUsuarioService : IReporteTareasUsuarioService
    {
        private const int AnioMinimo = 2000;
        private const int AnioMaximo = 2100;
        private const string Modulo = "Reportes";
        private const string ElementoConsulta = "reporte de tareas por usuario";
        private const string MensajeErrorInesperado = "Ocurrió un error inesperado. Por favor intente de nuevo.";
        private const string MensajePeriodoInvalido = "El mes y el año indicados no son válidos.";
        private readonly IReporteTareasUsuarioRepository _reporteRepository;
        private readonly IAuditoriaService _auditoriaService;

        public ReporteTareasUsuarioService(
            IReporteTareasUsuarioRepository reporteRepository,
            IAuditoriaService auditoriaService)
        {
            _reporteRepository = reporteRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<ServiceResult<IReadOnlyList<Usuario>>> ObtenerUsuariosAsync(int idUsuarioSesion)
        {
            try
            {
                var usuarios = (await _reporteRepository.ObtenerUsuariosAsync()).ToList();

                return new ServiceResult<IReadOnlyList<Usuario>> { IsSuccess = true, Data = usuarios };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult<IReadOnlyList<Usuario>> { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult<ReporteTareasResultado>> GenerarReporteAsync(int idUsuario, int mes, int anio, int idUsuarioSesion)
        {
            if (mes < 1 || mes > 12 || anio < AnioMinimo || anio > AnioMaximo)
            {
                return new ServiceResult<ReporteTareasResultado> { IsSuccess = false, Message = MensajePeriodoInvalido };
            }

            try
            {
                var desde = new DateTime(anio, mes, 1);
                var filas = (await _reporteRepository.ObtenerPorUsuarioMesAsync(idUsuario, desde, desde.AddMonths(1))).ToList();

                await _auditoriaService.RegistrarConsultaAsync(idUsuarioSesion, ElementoConsulta);

                return new ServiceResult<ReporteTareasResultado>
                {
                    IsSuccess = true,
                    Data = new ReporteTareasResultado
                    {
                        Filas = filas,
                        TotalHoras = filas.Sum(f => f.HorasInvertidas)
                    }
                };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult<ReporteTareasResultado> { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        private async Task RegistrarErrorSeguroAsync(int idUsuarioSesion, Exception ex)
        {
            try
            {
                int? idUsuario = idUsuarioSesion > 0 ? idUsuarioSesion : null;
                await _auditoriaService.RegistrarErrorAsync(idUsuario, Modulo, ex);
            }
            catch
            {
                
            }
        }
    }
}