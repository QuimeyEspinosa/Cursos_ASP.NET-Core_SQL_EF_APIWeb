using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class EditCarViewModel :CreateCarViewModel
    {
        public int Id { get; set; }
        public string ExistPathImg { get; set; }
    }
}
