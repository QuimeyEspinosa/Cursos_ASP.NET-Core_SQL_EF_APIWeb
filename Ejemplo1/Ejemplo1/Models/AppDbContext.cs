using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } //Propiedad para consultar y guardar instancias de la clase Producto
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Valida si existen datos, si no existen realiza un insert de un primer elemento
            modelBuilder.Entity<Producto>().HasData(new Producto 
            {
                Id = 1, Descripcion = "Cafe", Precio = 50f 
            });
        }

    }
}
