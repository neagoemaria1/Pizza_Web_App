using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class Cos_ProdusRepository : RepositoryBase<Cos_Produs>, ICos_ProdusRepository
    {
        public Cos_ProdusRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 