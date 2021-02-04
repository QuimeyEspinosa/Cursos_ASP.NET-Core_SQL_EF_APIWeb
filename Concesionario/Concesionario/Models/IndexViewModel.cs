using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class IndexViewModel : Paginator
    {
        public List<Car> Cars { get; set; }
    }
}
