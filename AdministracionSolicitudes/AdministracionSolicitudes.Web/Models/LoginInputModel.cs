using System.ComponentModel.DataAnnotations;

namespace AdministracionSolicitudes.Web.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}