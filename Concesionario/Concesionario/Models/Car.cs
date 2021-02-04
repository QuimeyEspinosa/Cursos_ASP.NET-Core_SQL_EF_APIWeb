using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Models
{
    public class Car
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Descripcion { get; set; }

        [RegularExpression(@"^\d{4}$")]
        public string Modelo { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        public string Estado { get; set; }

        public string PathImg { get; set; }

    }
}
