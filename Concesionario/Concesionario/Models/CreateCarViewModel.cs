using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class CreateCarViewModel
    {
        public List<SelectListItem> Marcas { get; set; }
        public int Marca { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Descripcion { get; set; }

        public string Modelo { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        public int Kilometros { get; set; }

        public string Estado { get; set; }

        public IFormFile Foto { get; set; }
    }
}
