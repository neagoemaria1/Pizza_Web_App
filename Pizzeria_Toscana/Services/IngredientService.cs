using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services
{ 
    public class IngredientService : IIngredientService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public IngredientService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public Ingredient GetIngredientByName(string denumire)
        {
            return _repositoryWrapper.IngredientRepository.FindByCondition(i => i.Denumire == denumire).FirstOrDefault();
        }
        public List<Ingredient> GetAllIngredients()
        {
            return _repositoryWrapper.IngredientRepository.FindAll().ToList();
        }

        public Ingredient GetIngredientByCode(int codIngredient)
        {
            return _repositoryWrapper.IngredientRepository.FindByCondition(i => i.COD_Ingredient == codIngredient).FirstOrDefault();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            _repositoryWrapper.IngredientRepository.Create(ingredient);
            _repositoryWrapper.Save();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            _repositoryWrapper.IngredientRepository.Update(ingredient);
            _repositoryWrapper.Save();
        }

        public void DeleteIngredient(int codIngredient)
        {
            var ingredient = _repositoryWrapper.IngredientRepository.FindByCondition(i => i.COD_Ingredient == codIngredient).FirstOrDefault();
            if (ingredient != null)
            {
                _repositoryWrapper.IngredientRepository.Delete(ingredient);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Ingredient with code {codIngredient} not found.");
            }
        }
    }
}