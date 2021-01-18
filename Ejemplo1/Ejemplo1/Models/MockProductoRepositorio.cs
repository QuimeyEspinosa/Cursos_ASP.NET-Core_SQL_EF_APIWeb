using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public class MockProductoRepositorio : IProductoComercio
    {
        private List<Producto> productosMock;

        public MockProductoRepositorio()
        {
            productosMock = new List<Producto>();

            productosMock.Add(new Producto() { Id = 1, Descripcion = "Cafe", Precio = 50f });
            productosMock.Add(new Producto() { Id = 2, Descripcion = "Medialuna", Precio = 20f });
            productosMock.Add(new Producto() { Id = 3, Descripcion = "Brownie", Precio = 80f });
            productosMock.Add(new Producto() { Id = 4, Descripcion = "Chocolate", Precio = 75f });
        }

        public Producto GetDatosProducto(int id)
        {
            return this.productosMock.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> GetProductos()
        {
            return productosMock;
        }

        public Producto AddProducto(Producto newProd)
        {
            newProd.Id = productosMock.Max(a => a.Id) + 1;
            productosMock.Add(newProd);
            return newProd;
        }

        public Producto EditProducto(Producto editProd)
        {
            Producto prod = productosMock.FirstOrDefault(e => e.Id == editProd.Id);

            if (prod != null)
            {
                prod.Descripcion = editProd.Descripcion;
                prod.Precio = editProd.Precio;
            }

            return prod;
        }
        public Producto DeleteProducto(int id)
        {
            Producto prod = productosMock.FirstOrDefault(e => e.Id == id);

            if (prod != null)
            {
                productosMock.Remove(prod);
            }

            return prod;
        }
    }
}
