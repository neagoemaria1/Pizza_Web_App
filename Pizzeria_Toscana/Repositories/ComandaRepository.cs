using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class ComandaRepository : RepositoryBase<Comanda>, IComandaRepository
    {
        public ComandaRepository(PizzerieContext pizzerieContext)
           : base(pizzerieContext)
        {
        }
    }
} 