using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class Paginator
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int SizePage { get; set; }
    }
}
