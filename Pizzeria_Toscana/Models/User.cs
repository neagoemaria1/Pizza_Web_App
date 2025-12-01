using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Pizzeria_Toscana.Models
{
    public class User : IdentityUser
    {

        public string? Nume { get; set; }
        public string? Prenume { get; set; }
        public string? Adresa { get; set; }
        public string? Telefon { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public Cos? Cos { get; set; }
         
        public ICollection<Comanda>? Comanda { get; set; }


    }
}

