using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoMora.Entidades;

namespace PrestamoMora.Data
{
    public class Contexto : DbContext 
    {
        public DbSet<Personas> personas { get; set; }
        public DbSet<Prestamos> prestamos { get; set; }
        public DbSet<Moras> moras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Data\\Prestamos.db");
            base.OnConfiguring(optionsBuilder);
        }
        
        
    }
}