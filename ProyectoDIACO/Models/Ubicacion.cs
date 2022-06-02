using System.ComponentModel.DataAnnotations;

namespace ProyectoDIACO.Models
{
    public class Ubicacion
    {
        [Key]
        public int UbicacionId { get; set; }

        public string Nombre { get; set; }

        public int Tipo { get; set; }

        public int? SububicacionId { get; set; }
    }
}
