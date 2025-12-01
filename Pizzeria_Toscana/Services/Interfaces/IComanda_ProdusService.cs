using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IComanda_ProdusService
    {
        List<Comanda_Produs> GetAllComenziProduse();
        Comanda_Produs GetComandaProdusById(int idComandaProdus);
        void AddComandaProdus(Comanda_Produs comandaProdus); 
        void UpdateComandaProdus(Comanda_Produs comandaProdus);
        void DeleteComandaProdus(int idComandaProdus);

    }
}
