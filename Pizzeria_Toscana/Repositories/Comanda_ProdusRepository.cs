using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class Comanda_ProdusRepository : RepositoryBase<Comanda_Produs>, IComanda_ProdusRepository
    {
        public Comanda_ProdusRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 