using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public class MockProductoComercio : IProductoComercio
    {
        private List<Producto> productosMock;

        public MockProductoComercio()
        {
            productosMock = new List<Producto>();

            productosMock.Add(new Producto() { Id = 1, Descripcion = "Cafe", Precio = 50f });
            productosMock.Add(new Producto() { Id = 2, Descripcion = "Medialuna", Precio = 20f });
            productosMock.Add(new Producto() { Id = 3, Descripcion = "Chocolate", Precio = 2.2f });
        }

        public Producto GetDatosProducto(int id)
        {
            return this.productosMock.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> GetProductos()
        {
            return productosMock;
        }
    }
}
