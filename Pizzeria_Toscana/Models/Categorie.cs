using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria_Toscana.Models
{
    public class Categorie
    {
        [Key]
        public int ID_Categorie { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nume { get; set; } 

        public ICollection<Produs>? Produse { get; set; }
    }
}