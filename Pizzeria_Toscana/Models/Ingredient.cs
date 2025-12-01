using System.ComponentModel.DataAnnotations;


namespace Pizzeria_Toscana.Models
{
    public class Ingredient
    {
        [Key]
        public int COD_Ingredient { get; set; }
        public string? Denumire { get; set; }
        public ICollection<Produs_Ingredient>? Produs_Ingredient { get; set; }
    }
}
 