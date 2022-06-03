using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDIACO.Models
{
    public class Queja
    {
        [Key]
        public int QuejaId { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        [MaxLength(500)]
        public string Detalle { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int? Estado { get; set; } = 0;

        public string? Resultado { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }


        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }

        [ForeignKey("SucursalId")]
        public Sucursal? Sucursal { get; set; }
    }
}
