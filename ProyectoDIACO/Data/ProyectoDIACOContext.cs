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

        public DbSet<ProyectoDIACO.Models.Usuario>? Usuario { get; set; }
    }
}
