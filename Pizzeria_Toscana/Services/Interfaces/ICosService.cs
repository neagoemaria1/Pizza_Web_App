using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface ICosService
    {
        List<Cos> GetAllCosuri();
        Cos GetCosById(int idCos);
        Cos GetCosByUserId(string userId);
        Task UpdateTotalPriceAsync(int idCos);
        void AddCos(Cos cos);
        void UpdateCos(Cos cos); 
        void DeleteCos(int idCos);

    }
}
