using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IIngredientService
    {
        List<Ingredient> GetAllIngredients();
        public Ingredient GetIngredientByName(string denumire);
        Ingredient GetIngredientByCode(int codIngredient);
        void AddIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
        void DeleteIngredient(int codIngredient);
    }
} 
