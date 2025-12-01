using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services
{ 
    public class CategorieService : ICategorieService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategorieService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Categorie> GetAllCategorii()
        {
            return _repositoryWrapper.Categorie_Repository.FindAll().ToList();
        }

        public Categorie GetCategorieById(int idCategorie)
        {
            return _repositoryWrapper.Categorie_Repository
                .FindByCondition(c => c.ID_Categorie == idCategorie)
                .FirstOrDefault();
        }

        public void AddCategorie(Categorie categorie)
        {
            if (categorie == null)
            {
                throw new ArgumentNullException(nameof(categorie), "Categorie cannot be null.");
            }

            _repositoryWrapper.Categorie_Repository.Create(categorie);
            _repositoryWrapper.Save();
        }

        public void UpdateCategorie(Categorie categorie)
        {
            if (categorie == null)
            {
                throw new ArgumentNullException(nameof(categorie), "Categorie cannot be null.");
            }

            _repositoryWrapper.Categorie_Repository.Update(categorie);
            _repositoryWrapper.Save();
        }

        public void DeleteCategorie(int idCategorie)
        {
            var categorie = _repositoryWrapper.Categorie_Repository
                .FindByCondition(c => c.ID_Categorie == idCategorie)
                .FirstOrDefault();

            if (categorie != null)
            {
                _repositoryWrapper.Categorie_Repository.Delete(categorie);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Categorie with ID {idCategorie} not found.");
            }
        }
    }
}
