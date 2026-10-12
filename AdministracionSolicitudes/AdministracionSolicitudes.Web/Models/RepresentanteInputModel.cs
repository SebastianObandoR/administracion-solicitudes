using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class RepresentanteInputModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        [StringLength(150, ErrorMessage = "El email no puede superar los 150 caracteres.")]
        public string Correo { get; set; } = string.Empty;
    }
}
