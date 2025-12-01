using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class Categorie_Repository : RepositoryBase<Categorie>, ICategorie_Repository
    {
        public Categorie_Repository(PizzerieContext pizzerieContext)
            : base(pizzerieContext)
        {
        } 
    }
}

