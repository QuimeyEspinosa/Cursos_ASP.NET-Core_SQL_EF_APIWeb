using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class IndexViewModel : Paginator
    {
        private List<CarViewModel> cars;

        public IndexViewModel()
        {
            cars = new List<CarViewModel>();
        }

        public List<CarViewModel> Cars 
        {
            get
            {
                return this.cars;
            }
            set
            {
                this.cars = value;
            }
        }
    }
}
