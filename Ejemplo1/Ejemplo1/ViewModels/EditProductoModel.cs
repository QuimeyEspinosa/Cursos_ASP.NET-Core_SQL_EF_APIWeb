using Ejemplo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.ViewModels
{
    public class EditProductoModel : CreateProductoModel
    {
        public int Id { get; set; }
        public string RutaFotoExistente { get; set; }
    }
}
