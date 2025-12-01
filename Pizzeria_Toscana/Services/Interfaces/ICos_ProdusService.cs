using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface ICos_ProdusService
    {
        List<Cos_Produs> GetAllCosProduse();
        Cos_Produs GetCosProdusById(int idCosProdus);
        List<Cos_Produs> GetAllCosProduseByCosId(int cosId);
        void AddCosProdus(Cos_Produs cosProdus);
        void UpdateCosProdus(Cos_Produs cosProdus); 
        void DeleteCosProdus(int idCosProdus);
        Task RemoveCosProdusAsync(Cos_Produs cosProdus);
        Cos_Produs GetCosProdusByCosIdAndProductId(int cosId, string productId);
        Task ClearCosProdusesForCosAsync(int cosId);
    }
}
