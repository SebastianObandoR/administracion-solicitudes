using System.ComponentModel.DataAnnotations;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class SolicitudValidacion : IValidatableObject
    {
        public const int AnioMinimo = 2026;
        public const int AnioMaximo = 2100;

        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "El consecutivo de oficio es requerido")]
        [StringLength(50)]
        public string ConsecutivoOficio { get; set; } = string.Empty;

        [Required(ErrorMessage = "El titulo es requerido")]
        [StringLength(300)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descripcion { get; set; }

        [StringLength(1000)]
        public string? Observaciones { get; set; }

        [StringLength(500)]
        public string? DocumentoInicio { get; set; }

        [StringLength(500)]
        public string? DocumentoRespuesta { get; set; }

        [Required(ErrorMessage = "El representante es requerido")]
        public int? IdRepresentante { get; set; }

        public int? IdEstado { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es requerida")]
        [DataType(DataType.Date)]
        public DateTime? FechaIngreso { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaRespuesta { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

        public static Dictionary<string, string[]> Validar(Solicitud solicitud, bool esEdicion)
        {
            var entrada = new SolicitudValidacion
            {
                ConsecutivoOficio = esEdicion ? "Sin cambios" : solicitud.ConsecutivoOficio,
                Titulo = solicitud.Titulo,
                Descripcion = solicitud.Descripcion,
                Observaciones = solicitud.Observaciones,
                DocumentoInicio = solicitud.DocumentoInicio,
                DocumentoRespuesta = solicitud.DocumentoRespuesta,
                IdRepresentante = solicitud.IdRepresentante > 0 ? solicitud.IdRepresentante : null,
                FechaIngreso = solicitud.FechaIngreso == default ? null : solicitud.FechaIngreso,
                FechaInicio = solicitud.FechaInicio,
                FechaRespuesta = solicitud.FechaRespuesta
            };
            var contexto = new ValidationContext(entrada);
            var errores = new List<ValidationResult>();
            Validator.TryValidateObject(entrada, contexto, errores, true);
            errores.AddRange(entrada.Validate(contexto));
            if (esEdicion && solicitud.IdEstado <= 0)
                errores.Add(new ValidationResult("El estado es requerido", new[] { nameof(IdEstado) }));
            return errores.SelectMany(error => error.MemberNames.Select(campo => new { Campo = campo, Mensaje = error.ErrorMessage! }))
                .GroupBy(error => error.Campo)
                .ToDictionary(grupo => grupo.Key, grupo => grupo.Select(error => error.Mensaje).Distinct().ToArray());
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string mensajeRango = $"debe estar entre el 01/01/{AnioMinimo} y el 31/12/{AnioMaximo}.";
            DateTime hoy = DateTime.Today;

            if (FechaIngreso.HasValue)
            {
                if (!EstaEnRango(FechaIngreso.Value))
                {
                    yield return new ValidationResult(
                        $"La fecha de ingreso {mensajeRango}",
                        new[] { nameof(FechaIngreso) });
                }
                else if (FechaIngreso.Value.Date > hoy)
                {
                    yield return new ValidationResult(
                        "La fecha de ingreso no puede ser futura.",
                        new[] { nameof(FechaIngreso) });
                }
            }

            if (FechaRespuesta.HasValue)
            {
                if (!EstaEnRango(FechaRespuesta.Value))
                {
                    yield return new ValidationResult(
                        $"La fecha de respuesta {mensajeRango}",
                        new[] { nameof(FechaRespuesta) });
                }
                else if (FechaRespuesta.Value.Date > hoy)
                {
                    yield return new ValidationResult(
                        "La fecha de respuesta no puede ser futura.",
                        new[] { nameof(FechaRespuesta) });
                }
            }

            if (FechaInicio.HasValue && !EstaEnRango(FechaInicio.Value))
            {
                yield return new ValidationResult(
                    $"La fecha de inicio {mensajeRango}",
                    new[] { nameof(FechaInicio) });
            }

            if (FechaRespuesta.HasValue && FechaIngreso.HasValue
                && FechaRespuesta.Value.Date < FechaIngreso.Value.Date)
            {
                yield return new ValidationResult(
                    "La fecha de respuesta no puede ser anterior a la fecha de ingreso.",
                    new[] { nameof(FechaRespuesta) });
            }

            if (FechaInicio.HasValue && FechaIngreso.HasValue
                && FechaInicio.Value.Date < FechaIngreso.Value.Date)
            {
                yield return new ValidationResult(
                    "La fecha de inicio no puede ser anterior a la fecha de ingreso.",
                    new[] { nameof(FechaInicio) });
            }
        }

        private static bool EstaEnRango(DateTime fecha)
            => fecha.Year >= AnioMinimo && fecha.Year <= AnioMaximo;
    }
}
