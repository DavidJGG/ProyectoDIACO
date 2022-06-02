using System.ComponentModel.DataAnnotations;

namespace ProyectoDIACO.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Campo obligatorio")]
        [EmailAddress(ErrorMessage ="Correo no valido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
