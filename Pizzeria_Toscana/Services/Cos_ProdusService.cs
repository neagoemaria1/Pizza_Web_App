using Microsoft.EntityFrameworkCore;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic; 
using System.Linq;

namespace Pizzeria_Toscana.Services
{
    public class Cos_ProdusService : ICos_ProdusService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public Cos_ProdusService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task RemoveCosProdusAsync(Cos_Produs cosProdus)
        {
            _repositoryWrapper.Cos_ProdusRepository.Delete(cosProdus);
            _repositoryWrapper.Save();
        }

        public List<Cos_Produs> GetAllCosProduse()
        {
            return _repositoryWrapper.Cos_ProdusRepository.FindAll().ToList();
        }

        public Cos_Produs GetCosProdusById(int idCosProdus)
        {
            return _repositoryWrapper.Cos_ProdusRepository.FindByCondition(cp => cp.ID_CosProdus == idCosProdus).FirstOrDefault();
        }

        public List<Cos_Produs> GetAllCosProduseByCosId(int cosId)
        {
            return _repositoryWrapper.Cos_ProdusRepository.FindByCondition(cp => cp.ID_Cos == cosId).ToList();
        }

        public void AddCosProdus(Cos_Produs cosProdus)
        {
            _repositoryWrapper.Cos_ProdusRepository.Create(cosProdus);
            _repositoryWrapper.Save();
        }

        public void UpdateCosProdus(Cos_Produs cosProdus)
        {
            _repositoryWrapper.Cos_ProdusRepository.Update(cosProdus);
            _repositoryWrapper.Save();
        }
        public async Task ClearCosProdusesForCosAsync(int cosId)
        {
            var cosProduses = _repositoryWrapper.Cos_ProdusRepository.FindByCondition(cp => cp.ID_Cos == cosId);
            foreach (var cosProdus in cosProduses)
            {
                _repositoryWrapper.Cos_ProdusRepository.Delete(cosProdus);
            }
            _repositoryWrapper.Save();
        }

        public void DeleteCosProdus(int idCosProdus)
        {
            var cosProdus = _repositoryWrapper.Cos_ProdusRepository.FindByCondition(cp => cp.ID_CosProdus == idCosProdus).FirstOrDefault();
            if (cosProdus != null)
            {
                _repositoryWrapper.Cos_ProdusRepository.Delete(cosProdus);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Cos_Produs with ID {idCosProdus} not found.");
            }
        }
        public Cos_Produs GetCosProdusByCosIdAndProductId(int cosId, string productId)
        {
            return _repositoryWrapper.Cos_ProdusRepository.FindByCondition(cp => cp.ID_Cos == cosId && cp.COD_Produs == productId).FirstOrDefault();
        }
    }
}