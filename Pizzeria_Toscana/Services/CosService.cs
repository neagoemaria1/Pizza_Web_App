using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services
{ 
    public class CosService : ICosService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CosService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Cos> GetAllCosuri()
        {
            return _repositoryWrapper.CosRepository.FindAll().ToList();
        }

        public Cos GetCosById(int idCos)
        {
            return _repositoryWrapper.CosRepository.FindByCondition(c => c.ID_Cos == idCos).FirstOrDefault();
        }

        public void AddCos(Cos cos)
        {
            _repositoryWrapper.CosRepository.Create(cos);
            _repositoryWrapper.Save();
        }

        public void UpdateCos(Cos cos)
        {
            _repositoryWrapper.CosRepository.Update(cos);
            _repositoryWrapper.Save();
        }

        public void DeleteCos(int idCos)
        {
            var cos = _repositoryWrapper.CosRepository.FindByCondition(c => c.ID_Cos == idCos).FirstOrDefault();
            if (cos != null)
            {
                _repositoryWrapper.CosRepository.Delete(cos);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Cos with ID {idCos} not found.");
            }
        }
        public async Task UpdateTotalPriceAsync(int idCos)
        {
            var cos = _repositoryWrapper.CosRepository.FindByCondition(c => c.ID_Cos == idCos)
                .FirstOrDefault();

            if (cos != null)
            {
                cos.Pret_total = cos.CosProdus.Sum(cp => cp.Pret * cp.Cantitate);
                _repositoryWrapper.CosRepository.Update(cos);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Cos with ID {idCos} not found.");
            }
        }
        public Cos GetCosByUserId(string userId)
        {
            return _repositoryWrapper.CosRepository.FindByCondition(c => c.ID_User.Equals(userId)).FirstOrDefault();
        }
    }
}