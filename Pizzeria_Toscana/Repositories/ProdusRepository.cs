using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class ProdusRepository : RepositoryBase<Produs>, IProdusRepository
    {
        public ProdusRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 