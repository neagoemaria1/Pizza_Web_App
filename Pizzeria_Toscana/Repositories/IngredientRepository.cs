using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 