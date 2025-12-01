using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IComandaService
    {
        List<Comanda> GetAllComenzi();
        Comanda GetComandaByNr(int nrComanda);
        void UpdateComanda(Comanda comanda);
        void DeleteComanda(int nrComanda); 
        void AddComanda(Comanda comanda);
        public int GetLastComandaNumber();
    }
}
