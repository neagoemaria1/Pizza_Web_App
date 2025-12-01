using Pizzeria_Toscana.Models;
using System.Collections.Generic;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface ICategorieService
    {
        List<Categorie> GetAllCategorii();
        Categorie GetCategorieById(int idCategorie);
        void AddCategorie(Categorie categorie);
        void UpdateCategorie(Categorie categorie); 
        void DeleteCategorie(int idCategorie);
    }
}
