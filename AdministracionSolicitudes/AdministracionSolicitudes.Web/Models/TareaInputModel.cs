using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class TareaInputModel : IValidatableObject
    {
        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(500, ErrorMessage = "La descripción no puede superar 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es requerida")]
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "Las horas invertidas son requeridas")]
        [Range(0.01, 999.99, ErrorMessage = "Las horas deben ser mayores a 0")]
        public decimal? HorasInvertidas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Fecha.HasValue && Fecha.Value.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "La fecha no puede ser anterior a hoy",
                    new[] { nameof(Fecha) });
            }

            if (!string.IsNullOrWhiteSpace(Descripcion) && !Descripcion.Any(char.IsLetter))
            {
                yield return new ValidationResult(
                    "La descripción debe contener al menos una letra",
                    new[] { nameof(Descripcion) });
            }
        }
    }
}