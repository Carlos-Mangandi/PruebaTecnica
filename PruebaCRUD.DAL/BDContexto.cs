using Microsoft.EntityFrameworkCore;
using PruebaCRUD.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCRUD.DAL
{
    public class BDContexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NFDMETJ\SQLEXPRESS;Initial Catalog=PruebaCRUD;Integrated Security=True");
        }
    }
}
