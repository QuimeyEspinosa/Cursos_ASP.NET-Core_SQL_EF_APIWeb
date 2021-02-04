﻿using Microsoft.AspNetCore.Http;
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
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Descripcion { get; set; }

        public string Modelo { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        public string Estado { get; set; }

        public IFormFile Foto { get; set; }
    }
}