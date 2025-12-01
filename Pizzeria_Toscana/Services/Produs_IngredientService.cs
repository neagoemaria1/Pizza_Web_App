using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services 
{
    public class Produs_IngredientService : IProdus_IngredientService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public Produs_IngredientService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Produs_Ingredient> GetAllProdus_Ingredients()
        {
            return _repositoryWrapper.Produs_IngredientRepository.FindAll().ToList();
        }

        public Produs_Ingredient GetProdus_IngredientByCode(int codProdusIngredient)
        {
            return _repositoryWrapper.Produs_IngredientRepository.FindByCondition(pi => pi.COD_Produs_Ingredient == codProdusIngredient).FirstOrDefault();
        }

        public void AddProdus_Ingredient(Produs_Ingredient produsIngredient)
        {
            _repositoryWrapper.Produs_IngredientRepository.Create(produsIngredient);
            _repositoryWrapper.Save();
        }

        public void UpdateProdus_Ingredient(Produs_Ingredient produsIngredient)
        {
            _repositoryWrapper.Produs_IngredientRepository.Update(produsIngredient);
            _repositoryWrapper.Save();
        }

        public void DeleteProdus_Ingredient(int codProdusIngredient)
        {
            var produsIngredient = _repositoryWrapper.Produs_IngredientRepository.FindByCondition(pi => pi.COD_Produs_Ingredient == codProdusIngredient).FirstOrDefault();
            if (produsIngredient != null)
            {
                _repositoryWrapper.Produs_IngredientRepository.Delete(produsIngredient);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Produs_Ingredient with code {codProdusIngredient} not found.");
            }
        }
    }
}