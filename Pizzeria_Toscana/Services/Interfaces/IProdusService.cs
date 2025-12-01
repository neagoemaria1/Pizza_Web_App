using Pizzeria_Toscana.Models;
namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IProdusService
    {
        string GetNextProdusCode();
        List<Produs> GetAllProduse();
        Produs GetProdusByCod(string codProdus);
        Produs GetProdusWithIngredientsByCod(string cod);
        void AddProdus(Produs produs);
        void UpdateProdus(Produs produs);
        void DeleteProdus(string codProdus);
    }
} 