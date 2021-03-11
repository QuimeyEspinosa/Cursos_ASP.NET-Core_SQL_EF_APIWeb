using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class IndexViewModel : Paginator
    {
        private List<CarViewModel> cars;
        private List<Marca> marcas;

        public IndexViewModel()
        {
            cars = new List<CarViewModel>();
            marcas = new List<Marca>();
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

        public List<Marca> Marcas
        {
            get
            {
                return this.marcas;
            }
            set
            {
                this.marcas = value;
            }
        }
    }
}
