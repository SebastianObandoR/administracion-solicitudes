using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AdministracionSolicitudes.Web.Models
{
    public class SolicitudInputModel : IValidatableObject
    {
        public const int AnioMinimo = 2026;
        public const int AnioMaximo = 2100;

        private const string FormatoHtml = "yyyy-MM-dd";

        public static string FechaMinimaHtml => new DateTime(AnioMinimo, 1, 1).ToString(FormatoHtml, CultureInfo.InvariantCulture);
        public static string FechaMaximaHtml => new DateTime(AnioMaximo, 12, 31).ToString(FormatoHtml, CultureInfo.InvariantCulture);
        public static string HoyHtml => DateTime.Today.ToString(FormatoHtml, CultureInfo.InvariantCulture);

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
