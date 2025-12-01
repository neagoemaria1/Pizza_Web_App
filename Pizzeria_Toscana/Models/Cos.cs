using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria_Toscana.Models
{
    public class Cos
    {
        [Key]
        public int ID_Cos { get; set; }
        public int Pret_total { get; set; }
        [ForeignKey("User")]
        public string? ID_User { get; set; }
        public ICollection<Cos_Produs>? CosProdus { get; set; }
        public ICollection<Comanda>? Comanda { get; set; }

    }
}
