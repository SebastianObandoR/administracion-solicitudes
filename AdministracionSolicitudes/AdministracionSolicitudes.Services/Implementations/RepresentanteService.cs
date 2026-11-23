using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class RepresentanteService : IRepresentanteService
    {
        private const int TamanoPagina = 10;
        private const string Modulo = "Representantes";
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        public RepresentanteService(
            IRepresentanteRepository representanteRepository,
            IErrorLogRepository errorLogRepository)
        {
            _representanteRepository = representanteRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<RepresentantePagina> ObtenerPaginaAsync(int pagina, int usuarioId, int? idRepresentante = null)
        {
            pagina = Math.Max(pagina, 1);
            return await EjecutarAsync(async () =>
            {
                var resultado = await _representanteRepository.ObtenerPaginaAsync(pagina, TamanoPagina, usuarioId, idRepresentante);
                return new RepresentantePagina
                {
                    Representantes = resultado.Representantes,
                    Pagina = resultado.Pagina,
                    TamanoPagina = TamanoPagina,
                    Total = resultado.Total
                };
            }, usuarioId);
        }

        public Task<Representante?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.ObtenerPorIdAsync(id, usuarioId), usuarioId);
        }

        public async Task<(Representante? Representante, Dictionary<string, string[]> Errores)> GuardarAsync(
            RepresentanteInputModel input, int usuarioId, int? id = null)
        {
            var validaciones = new List<ValidationResult>();
            if (!Validator.TryValidateObject(input, new ValidationContext(input), validaciones, validateAllProperties: true))
            {
                var errores = validaciones
                    .SelectMany(resultado => resultado.MemberNames.Select(campo => (Campo: campo, Mensaje: resultado.ErrorMessage!)))
                    .GroupBy(error => error.Campo)
                    .ToDictionary(grupo => grupo.Key, grupo => grupo.Select(error => error.Mensaje).ToArray());
                return (null, errores);
            }

            var representante = new Representante
            {
                IdRepresentante = id.GetValueOrDefault(),
                Nombre = input.Nombre.Trim(),
                Correo = input.Correo.Trim()
            };

            if (id is null)
            {
                if (await EjecutarAsync(() => _representanteRepository.CrearAsync(representante, usuarioId), usuarioId))
                {
                    return (representante, []);
                }
            }
            else
            {
                var resultado = await EjecutarAsync(
                    () => _representanteRepository.ActualizarAsync(representante, usuarioId), usuarioId);
                if (resultado != ActualizarRepresentanteResultado.CorreoDuplicado)
                {
                    return (resultado == ActualizarRepresentanteResultado.Actualizado ? representante : null, []);
                }
            }

            return (null, new Dictionary<string, string[]>
            {
                [nameof(input.Correo)] = ["Ya existe un representante con ese email."]
            });
        }

        public Task<EliminarRepresentanteResultado> EliminarAsync(int id, int usuarioId)
        {
            return EjecutarAsync(() => _representanteRepository.EliminarAsync(id, usuarioId), usuarioId);
        }

        private async Task<T> EjecutarAsync<T>(Func<Task<T>> operacion, int usuarioId)
        {
            try
            {
                return await operacion();
            }
            catch (DbException exception)
            {
                try
                {
                    await _errorLogRepository.RegistrarAsync(new ErrorLog
                    {
                        IdUsuario = usuarioId,
                        Modulo = Modulo,
                        MensajeError = exception.Message
                    });
                }
                catch (DbException errorLogException)
                {
                    throw new AggregateException(
                        "La operación falló y también falló el registro del error técnico.",
                        exception,
                        errorLogException);
                }

                throw;
            }
        }
    }
}
