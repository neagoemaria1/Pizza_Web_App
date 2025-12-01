using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IProdus_IngredientService
    {
        List<Produs_Ingredient> GetAllProdus_Ingredients();
        Produs_Ingredient GetProdus_IngredientByCode(int codProdusIngredient);
        void AddProdus_Ingredient(Produs_Ingredient produsIngredient);
        void UpdateProdus_Ingredient(Produs_Ingredient produsIngredient);
        void DeleteProdus_Ingredient(int codProdusIngredient);
    }
}
 