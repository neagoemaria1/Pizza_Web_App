using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class CosRepository : RepositoryBase<Cos>, ICosRepository
    {
        public CosRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
}
 