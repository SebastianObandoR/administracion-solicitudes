using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class TareaService : ITareaService
    {
        private const int TamanoPagina = 10;
        private const string Modulo = "Tareas";
        private const string MensajeErrorInesperado = "Ocurrió un error inesperado. Por favor intente de nuevo.";
        private const string MensajeDatosRelacionados = "No se puede eliminar un registro con datos relacionados.";
        private const string MensajeSolicitudNoExiste = "La solicitud indicada no existe.";
        private const string MensajeTareaNoExiste = "La tarea indicada no existe.";
        private readonly ITareaRepository _tareaRepository;
        private readonly IAuditoriaService _auditoriaService;

        public TareaService(ITareaRepository tareaRepository, IAuditoriaService auditoriaService)
        {
            _tareaRepository = tareaRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<ServiceResult<IReadOnlyList<SolicitudResumen>>> ObtenerSolicitudesAsync(int idUsuarioSesion)
        {
            try
            {
                var solicitudes = (await _tareaRepository.ObtenerSolicitudesAsync()).ToList();

                return new ServiceResult<IReadOnlyList<SolicitudResumen>>
                {
                    IsSuccess = true,
                    Data = solicitudes
                };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult<IReadOnlyList<SolicitudResumen>> { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult<TareasListado>> ObtenerTareasPorSolicitudAsync(int idSolicitud, int pagina, int idUsuarioSesion)
        {
            try
            {
                var solicitud = await _tareaRepository.ObtenerSolicitudAsync(idSolicitud);

                if (solicitud == null) return new ServiceResult<TareasListado> { IsSuccess = false, Message = MensajeSolicitudNoExiste };

                int totalElementos = await _tareaRepository.ContarSolicitudAsync(idSolicitud);
                int totalPaginas = Math.Max(1, (int)Math.Ceiling(totalElementos / (double)TamanoPagina));
                int paginaActual = Math.Clamp(pagina, 1, totalPaginas);
                var elementos = (await _tareaRepository.ObtenerSolicitudPaginacionAsync(idSolicitud, paginaActual, TamanoPagina)).ToList();

                await _auditoriaService.RegistrarConsultaAsync(idUsuarioSesion, Modulo);

                return new ServiceResult<TareasListado>
                {
                    IsSuccess = true,
                    Data = new TareasListado
                    {
                        Solicitud = solicitud,
                        Tareas = new PagedResult<Tarea>
                        {
                            Items = elementos,
                            Page = paginaActual,
                            PageSize = TamanoPagina,
                            TotalItems = totalElementos,
                            TotalPages = totalPaginas
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult<TareasListado> { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult<TareaFormulario>> ObtenerFormularioAsync(int idSolicitud, int? idTarea, int idUsuarioSesion)
        {
            try
            {
                var solicitud = await _tareaRepository.ObtenerSolicitudAsync(idSolicitud);

                if (solicitud == null) return new ServiceResult<TareaFormulario> { IsSuccess = false, Message = MensajeSolicitudNoExiste };

                Tarea? tarea = null;

                if (idTarea.HasValue)
                {
                    tarea = await _tareaRepository.ObtenerIdAsync(idTarea.Value);

                    if (tarea == null || tarea.IdSolicitud != idSolicitud) return new ServiceResult<TareaFormulario> { IsSuccess = false, Message = MensajeTareaNoExiste };
                }

                var usuarios = (await _tareaRepository.ObtenerUsuariosAsync()).ToList();

                return new ServiceResult<TareaFormulario>
                {
                    IsSuccess = true,
                    Data = new TareaFormulario { Solicitud = solicitud, Tarea = tarea, Usuarios = usuarios }
                };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult<TareaFormulario> { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult> CrearAsync(Tarea tarea, int idUsuarioSesion)
        {
            try
            {
                tarea.Descripcion = tarea.Descripcion.Trim();
                tarea.IdTarea = await _tareaRepository.InsertarAsync(tarea);

                await _auditoriaService.RegistrarCreacionAsync(idUsuarioSesion, Modulo, tarea);

                return new ServiceResult { IsSuccess = true, Message = "Tarea creada correctamente." };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult> ActualizarAsync(Tarea tarea, int idUsuarioSesion)
        {
            try
            {
                var anterior = await _tareaRepository.ObtenerIdAsync(tarea.IdTarea);

                if (anterior == null) return new ServiceResult { IsSuccess = false, Message = MensajeTareaNoExiste };

                tarea.IdSolicitud = anterior.IdSolicitud;
                tarea.Descripcion = tarea.Descripcion.Trim();

                bool actualizada = await _tareaRepository.ActualizarAsync(tarea);

                if (!actualizada) return new ServiceResult { IsSuccess = false, Message = MensajeTareaNoExiste };

                await _auditoriaService.RegistrarActualizacionAsync(idUsuarioSesion, Modulo, anterior, tarea);

                return new ServiceResult { IsSuccess = true, Message = "Tarea actualizada correctamente." };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        public async Task<ServiceResult> EliminarAsync(int idTarea, int idUsuarioSesion)
        {
            try
            {
                var tarea = await _tareaRepository.ObtenerIdAsync(idTarea);

                if (tarea == null) return new ServiceResult { IsSuccess = false, Message = MensajeTareaNoExiste };

                var resultado = await _tareaRepository.EliminarAsync(idTarea);

                if (resultado == ResultadoEliminacion.TieneRelacionados) return new ServiceResult { IsSuccess = false, Message = MensajeDatosRelacionados };

                if (resultado == ResultadoEliminacion.NoEncontrado) return new ServiceResult { IsSuccess = false, Message = MensajeTareaNoExiste };

                await _auditoriaService.RegistrarEliminacionAsync(idUsuarioSesion, Modulo, tarea);

                return new ServiceResult { IsSuccess = true, Message = "Tarea eliminada correctamente." };
            }
            catch (Exception ex)
            {
                await RegistrarErrorSeguroAsync(idUsuarioSesion, ex);
                return new ServiceResult { IsSuccess = false, Message = MensajeErrorInesperado };
            }
        }

        private async Task RegistrarErrorSeguroAsync(int idUsuarioSesion, Exception ex)
        {
            Console.WriteLine("=== ERROR TAREAS ===");
            Console.WriteLine(ex.ToString());

            try
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioSesion, Modulo, ex);
            }
            catch (Exception exLog)
            {
                Console.WriteLine("=== FALLO EL REGISTRO DEL ERROR ===");
                Console.WriteLine(exLog.Message);
            }
        }
    }
}