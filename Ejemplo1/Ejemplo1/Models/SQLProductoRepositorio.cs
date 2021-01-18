using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public class SQLProductoRepositorio : IProductoComercio
    {
        private readonly AppDbContext context;
        private List<Producto> listaProductos;

        public SQLProductoRepositorio(AppDbContext context)
        {
            this.context = context;
        }

        public Producto AddProducto(Producto newProd)
        {
            context.Productos.Add(newProd);
            context.SaveChanges();

            return newProd;
        }

        public Producto DeleteProducto(int id)
        {
            Producto prod = context.Productos.Find(id);

            if(prod != null)
            {
                context.Productos.Remove(prod);
                context.SaveChanges();
            }

            return prod;
        }

        public Producto EditProducto(Producto editProd)
        {
            var employee = context.Productos.Attach(editProd);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return editProd;
        }

        public Producto GetDatosProducto(int id)
        {
            return context.Productos.Find(id);
        }

        public List<Producto> GetProductos()
        {
            listaProductos = context.Productos.ToList<Producto>();

            return listaProductos;
        }
    }
}
