using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria_Toscana.Models
{
    public class Comanda
    {
        [Key]
        public int NR_Comanda { get; set; }
        public string? Descriere { get; set; }
        public bool Status { get; set; }
        public int Suma { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int? CosID_Cos { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public User? User { get; set; }


        public Cos? Cos { get; set; }

        public ICollection<Comanda_Produs>? Comanda_Produs { get; set; }

    }
}
