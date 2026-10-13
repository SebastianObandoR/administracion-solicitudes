using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class EstadoSolicitudInputModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
    }
}
