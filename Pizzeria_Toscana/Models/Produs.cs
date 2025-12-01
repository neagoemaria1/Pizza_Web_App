using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria_Toscana.Models
{
    public class Produs
    {
        [Key]
        public string? COD_Produs { get; set; }
        public string? Denumire { get; set; }

        [MaxLength(500)]
        public string? Descriere { get; set; }

        public int Pret { get; set; }
        public byte[]? ProdusPicture { get; set; }

        [ForeignKey("Categorie")]
        public int ID_Categorie { get; set; }
        public Categorie? Categorie { get; set; }

        public ICollection<Produs_Ingredient>? Produs_Ingredient { get; set; }

        public ICollection<Comanda_Produs>? Comanda_Produs { get; set; }

        public ICollection<Cos_Produs>? CosProdus { get; set; }

       
    }
}
