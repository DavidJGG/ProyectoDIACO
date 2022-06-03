using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoDIACO.Models;

namespace ProyectoDIACO.Data
{
    public class ProyectoDIACOContext : DbContext
    {
        public ProyectoDIACOContext (DbContextOptions<ProyectoDIACOContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ubicacion>().HasData(
                new Ubicacion { UbicacionId=1, Nombre = "Suroccidente", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=2, Nombre = "Metropolitana", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=3, Nombre = "Noroccidente", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=4, Nombre = "Central", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=5, Nombre = "Verapaz", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=6, Nombre = "Nororiente", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=7, Nombre = "Suroriente", SububicacionId = null, Tipo = 1},
                new Ubicacion { UbicacionId=8, Nombre = "Petén", SububicacionId = null, Tipo = 1},

                new Ubicacion { UbicacionId=9, Nombre = "Quetzaltenango", SububicacionId = 1, Tipo = 2},
                new Ubicacion { UbicacionId = 10, Nombre = "Guatemala", SububicacionId = 2, Tipo = 2},
                new Ubicacion { UbicacionId = 11, Nombre = "Huehuetenango", SububicacionId = 3, Tipo = 2 },
                new Ubicacion { UbicacionId = 12, Nombre = "Chimaltenango", SububicacionId = 4, Tipo = 2 },
                new Ubicacion { UbicacionId = 13, Nombre = "Alta Verapaz", SububicacionId = 5, Tipo = 2 },
                new Ubicacion { UbicacionId = 14, Nombre = "Chiquimula", SububicacionId = 6, Tipo = 2 },
                new Ubicacion { UbicacionId = 15, Nombre = "Jutiapa", SububicacionId = 7, Tipo = 2 },
                new Ubicacion { UbicacionId = 16, Nombre = "Petén", SububicacionId = 8, Tipo = 2 },

                new Ubicacion { UbicacionId = 17, Nombre = "Quetzaltenango", SububicacionId = 9, Tipo = 3 },
                new Ubicacion { UbicacionId = 18, Nombre = "Guatemala", SububicacionId = 10, Tipo = 3 },
                new Ubicacion { UbicacionId = 19, Nombre = "Huehuetenango", SububicacionId = 11, Tipo = 3 },
                new Ubicacion { UbicacionId = 20, Nombre = "Chimaltenango", SububicacionId = 12, Tipo = 3 },
                new Ubicacion { UbicacionId = 21, Nombre = "Coban", SububicacionId = 13, Tipo = 3 },
                new Ubicacion { UbicacionId = 22, Nombre = "Chiquimula", SububicacionId = 14, Tipo = 3 },
                new Ubicacion { UbicacionId = 23, Nombre = "Jutiapa", SububicacionId = 15, Tipo = 3 },
                new Ubicacion { UbicacionId = 24, Nombre = "Flores", SububicacionId = 16, Tipo = 3 }


            );
        }

        public DbSet<ProyectoDIACO.Models.Usuario>? Usuario { get; set; }

        public DbSet<ProyectoDIACO.Models.Ubicacion>? Ubicacion { get; set; }

        public DbSet<ProyectoDIACO.Models.Comercio>? Comercio { get; set; }

        public DbSet<ProyectoDIACO.Models.Sucursal>? Sucursal { get; set; }

        public DbSet<ProyectoDIACO.Models.Queja>? Queja { get; set; }
    }
}
