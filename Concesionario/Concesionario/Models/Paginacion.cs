using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class Paginacion
    {
        public int Page { get; set; } = 1;
        public int ItemsToShow { get; set; } = 3;
    }
}
