using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Models
{
    public class CreateProductoModel
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public float Precio { get; set; }

        public IFormFile Foto { get; set; }
    }
}
