using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria_Toscana.Services
{
    public class ProdusService : IProdusService  
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProdusService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }


        public string GetNextProdusCode()
        {
            var allCoduri = _repositoryWrapper.ProdusRepository
                .FindAll()
                .Select(p => p.COD_Produs)
                .ToList();

            int maxNumericPart = 0;

            foreach (var cod in allCoduri)
            {
                // Extragem partea numerica din cod
                var numericPart = new string(cod.Where(char.IsDigit).ToArray());

                if (int.TryParse(numericPart, out int numericValue))
                {
                    maxNumericPart = Math.Max(maxNumericPart, numericValue);
                }
            }

            // Generam urmatorul cod alfanumeric
            return $"PROD{maxNumericPart + 1:D3}";
        }
        public List<Produs> GetAllProduse()
        {
            return _repositoryWrapper.ProdusRepository.FindAll().ToList();
        }

        public Produs GetProdusByCod(string codProdus)
        {
            return _repositoryWrapper.ProdusRepository.FindByCondition(p => p.COD_Produs == codProdus).FirstOrDefault();
        }
        public Produs GetProdusWithIngredientsByCod(string cod)
        {
            return _repositoryWrapper.ProdusRepository.FindByCondition(p => p.COD_Produs == cod)
                .Include(p => p.Produs_Ingredient)
                    .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefault();
        }

        public void AddProdus(Produs produs)
        {
            _repositoryWrapper.ProdusRepository.Create(produs);
            _repositoryWrapper.Save();
        }

        public void UpdateProdus(Produs produs)
        {
            _repositoryWrapper.ProdusRepository.Update(produs);
            _repositoryWrapper.Save();
        }

        public void DeleteProdus(string codProdus)
        {
            var produs = _repositoryWrapper.ProdusRepository.FindByCondition(p => p.COD_Produs == codProdus).FirstOrDefault();
            if (produs != null)
            {
                _repositoryWrapper.ProdusRepository.Delete(produs);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Produs with COD {codProdus} not found.");
            }
        }
    }
}