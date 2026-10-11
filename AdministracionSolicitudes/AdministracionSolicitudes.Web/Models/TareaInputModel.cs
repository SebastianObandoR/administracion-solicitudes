using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class TareaInputModel
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
        [Range(typeof(decimal), "0.01", "999.99", ErrorMessage = "Las horas deben ser mayores a 0")]
        public decimal? HorasInvertidas { get; set; }
    }
}