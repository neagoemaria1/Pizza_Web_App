using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services
{
    public class Comanda_ProdusService : IComanda_ProdusService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
         
        public Comanda_ProdusService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Comanda_Produs> GetAllComenziProduse()
        {
            return _repositoryWrapper.Comanda_ProdusRepository.FindAll().ToList();
        }

        public Comanda_Produs GetComandaProdusById(int idComandaProdus)
        {
            return _repositoryWrapper.Comanda_ProdusRepository.FindByCondition(cp => cp.ID_Comanda_Produs == idComandaProdus).FirstOrDefault();
        }

        public void AddComandaProdus(Comanda_Produs comandaProdus)
        {
            _repositoryWrapper.Comanda_ProdusRepository.Create(comandaProdus);
            _repositoryWrapper.Save();
        }

        public void UpdateComandaProdus(Comanda_Produs comandaProdus)
        {
            _repositoryWrapper.Comanda_ProdusRepository.Update(comandaProdus);
            _repositoryWrapper.Save();
        }

        public void DeleteComandaProdus(int idComandaProdus)
        {
            var comandaProdus = _repositoryWrapper.Comanda_ProdusRepository.FindByCondition(cp => cp.ID_Comanda_Produs == idComandaProdus).FirstOrDefault();
            if (comandaProdus != null)
            {
                _repositoryWrapper.Comanda_ProdusRepository.Delete(comandaProdus);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Comanda_Produs with ID {idComandaProdus} not found.");
            }
        }
    }
}