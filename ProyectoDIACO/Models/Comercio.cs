using System.ComponentModel.DataAnnotations;

namespace ProyectoDIACO.Models
{
    public class Comercio
    {
        [Key]
        public int ComercioId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        [Range(10000000, 99999999, ErrorMessage = "No parece ser un número valido")]
        public long Telefono { get; set; }

        public int? Estado { get; set; } = 1;
    }
}
