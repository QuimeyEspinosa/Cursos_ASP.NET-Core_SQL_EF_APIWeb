using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class Marca
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Car> Cars { get; set; }
    }
}
