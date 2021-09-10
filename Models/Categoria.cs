using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
