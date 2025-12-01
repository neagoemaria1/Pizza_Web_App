using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria_Toscana.Models
{
    public class Comanda_Produs
    {
        [Key]
        public int ID_Comanda_Produs { get; set; }
        public int Cantitate { get; set; }

            [ForeignKey("Comanda")]
        public int ComandaId { get; set; }
        public Comanda? Comanda { get; set; }

        [ForeignKey("Produs")]
        public string? ProdusId { get; set; }
        public Produs? Produs { get; set; }
    }
}
