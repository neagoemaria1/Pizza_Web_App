using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class Produs_IngredientRepository : RepositoryBase<Produs_Ingredient>, IProdus_IngredientRepository
    {
        public Produs_IngredientRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 