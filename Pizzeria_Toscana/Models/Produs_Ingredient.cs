using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria_Toscana.Models
{
    public class Produs_Ingredient
    {
        [Key]
        public int COD_Produs_Ingredient { get; set; }
        [MaxLength(16)] public string? Cantitate_Ingredient { get; set; }

        [ForeignKey("Produs")]
        public string? COD_Produs { get; set; }
        public Produs? Produs { get; set; }

        [ForeignKey("Ingredient")]
        public int COD_Ingredient { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
