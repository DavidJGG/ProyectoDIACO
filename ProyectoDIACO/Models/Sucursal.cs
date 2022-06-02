using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDIACO.Models
{
    public class Sucursal
    {
        [Key]
        public int SucursalId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(10000000, 99999999, ErrorMessage = "No parece ser un número valido")]
        public long Telefono { get; set; }

        public int? Estado { get; set; } = 1;

        [ForeignKey("Ubicacion")]
        public int MunicipioId { get; set; }

        [ForeignKey("MunicipioId")]
        public Ubicacion? Ubicacion { get; set; }


        [ForeignKey("Comercio")]
        public int ComercioId { get; set; }

        [ForeignKey("ComercioId")]
        public Comercio? Comercio { get; set; }
    }
}
