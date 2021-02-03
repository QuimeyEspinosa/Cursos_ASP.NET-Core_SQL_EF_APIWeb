using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Concesionario.Data;
using System;
using System.Linq;

namespace Concesionario.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CarContext>>()))
            {
                // Look for any movies.
                if (context.Car.Any())
                {
                    return;   // DB has been seeded
                }

                context.Car.AddRange(
                    new Car
                    {
                        Descripcion = "Chevrolet Onix",
                        Modelo = "2019",
                        Precio = 890000,
                        Estado = "Usado"
                    },

                    new Car
                    {
                        Descripcion = "Ford Focus III",
                        Modelo = "2014",
                        Precio = 1200000,
                        Estado = "Usado"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}