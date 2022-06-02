using System.ComponentModel.DataAnnotations;

namespace ProyectoDIACO.Models
{
    public class Usuario
    {
        [Key]
        public int usuarioId { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El {0} no es una dirección de correo electrónico válida.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Display(Name = "Contraseña")]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
        [MaxLength(256, ErrorMessage = "Máximo 50 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set;}

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [MinLength(8, ErrorMessage = "El nit debe tener 8 elementos sin guiones")]
        [MaxLength(8, ErrorMessage = "El nit debe tener 8 elementos sin guiones")]
        public string Nit { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public int? Rol { get; set; }

        public int? Estado { get; set; } = 1;
    }
}
