using global::Pizzeria_Toscana.Models;
using global::Pizzeria_Toscana.Repositories.Interfaces;
using global::Pizzeria_Toscana.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services 
{ 
    public class ComandaService : IComandaService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ComandaService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Comanda> GetAllComenzi()
        {
            return _repositoryWrapper.ComandaRepository.FindAll().ToList();
        }

        public Comanda GetComandaByNr(int nrComanda)
        {
            return _repositoryWrapper.ComandaRepository.FindByCondition(c => c.NR_Comanda == nrComanda).FirstOrDefault();
        }

        public void AddComanda(Comanda comanda)
        {
            _repositoryWrapper.ComandaRepository.Create(comanda);
            _repositoryWrapper.Save();
        }
        public int GetLastComandaNumber()
        {
            var lastComanda = _repositoryWrapper.ComandaRepository.FindByCondition(c => true).OrderByDescending(c => c.NR_Comanda).FirstOrDefault();
            return lastComanda?.NR_Comanda ?? 1;
        }
        public void UpdateComanda(Comanda comanda)
        {
            _repositoryWrapper.ComandaRepository.Update(comanda);
            _repositoryWrapper.Save();
        }

        public void DeleteComanda(int nrComanda)
        {
            var comanda = _repositoryWrapper.ComandaRepository.FindByCondition(c => c.NR_Comanda == nrComanda).FirstOrDefault();
            if (comanda != null)
            {
                _repositoryWrapper.ComandaRepository.Delete(comanda);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"Comanda with number {nrComanda} not found.");
            }
        }
    }
}