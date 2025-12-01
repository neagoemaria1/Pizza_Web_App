using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria_Toscana.Models
{
    public class Cos_Produs
    {
        [Key]
        public int ID_CosProdus { get; set; }
        public int Cantitate { get; set; }
        public int Pret { get; set; } 

        [ForeignKey("Cos")]
        public int ID_Cos { get; set; }
        public Cos? Cos { get; set; }

        [ForeignKey("Produs")]
        public string COD_Produs { get; set; }
        public Produs? Produs { get; set; }

    }
}
