using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class SolicitudInputModel : IValidatableObject
    {
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
        public int IdRepresentante { get; set; }

        public int IdEstado { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es requerida")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime? FechaRespuesta { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaRespuesta.HasValue && FechaRespuesta.Value.Date < FechaIngreso.Date)
            {
                yield return new ValidationResult(
                    "La fecha de respuesta no puede ser anterior a la fecha de ingreso.",
                    new[] { nameof(FechaRespuesta) });
            }

            if (FechaInicio.HasValue && FechaInicio.Value.Date < FechaIngreso.Date)
            {
                yield return new ValidationResult(
                    "La fecha de inicio no puede ser anterior a la fecha de ingreso.",
                    new[] { nameof(FechaInicio) });
            }
        }
    }
}
