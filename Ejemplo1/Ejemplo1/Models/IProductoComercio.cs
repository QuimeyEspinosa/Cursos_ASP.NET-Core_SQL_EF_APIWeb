using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public interface IProductoComercio
    {
        Producto GetDatosProducto(int id);

        List<Producto> GetProductos();
    }
}
