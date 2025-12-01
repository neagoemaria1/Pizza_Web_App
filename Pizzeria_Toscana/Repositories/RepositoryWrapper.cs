using Microsoft.EntityFrameworkCore;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories.Interfaces;

namespace Pizzeria_Toscana.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private PizzerieContext _pizzerieContext;
        private IProdusRepository? _produsRepository;
        private IComanda_ProdusRepository? _comanda_ProdusRepository;
        private IComandaRepository? _comandaRepository;
        private ICosRepository? _cosRepository;
        private ICos_ProdusRepository? cos_ProdusRepository;
        private IIngredientRepository? _ingredientRepository;
        private IProdus_IngredientRepository? produs_IngredientRepository;
        private ICategorie_Repository? _categorie_Repository;
        private IUserRepository? _userRepository;
     
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null) 
                {
                    _userRepository = new UserRepository(_pizzerieContext);
                }

                return _userRepository;
            }
        }


        public IProdus_IngredientRepository Produs_IngredientRepository
        {
            get
            {
                if (produs_IngredientRepository == null)
                {
                    produs_IngredientRepository = new Produs_IngredientRepository(_pizzerieContext);
                }

                return produs_IngredientRepository;
            }
        }

        public IIngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository(_pizzerieContext);
                }

                return _ingredientRepository;
            }
        }

        public ICos_ProdusRepository Cos_ProdusRepository
        {
            get
            {
                if (cos_ProdusRepository == null)
                {
                    cos_ProdusRepository = new Cos_ProdusRepository(_pizzerieContext);
                }

                return cos_ProdusRepository;
            }
        }
        public ICosRepository CosRepository
        {
            get
            {
                if (_cosRepository == null)
                {
                    _cosRepository = new CosRepository(_pizzerieContext);
                }

                return _cosRepository;
            }
        }
        public IComandaRepository ComandaRepository
        {
            get
            {
                if (_comandaRepository == null)
                {
                    _comandaRepository = new ComandaRepository(_pizzerieContext);
                }

                return _comandaRepository;
            }
        }

        public IComanda_ProdusRepository Comanda_ProdusRepository
        {
            get
            {
                if (_comanda_ProdusRepository == null)
                {
                    _comanda_ProdusRepository = new Comanda_ProdusRepository(_pizzerieContext);
                }

                return _comanda_ProdusRepository;
            }
        }
        public IProdusRepository ProdusRepository
        {
            get
            {
                if (_produsRepository == null)
                {
                    _produsRepository = new ProdusRepository(_pizzerieContext);
                }

                return _produsRepository;
            }
        }
        public ICategorie_Repository Categorie_Repository
        {
            get
            {
                if (_categorie_Repository == null)
                {
                    _categorie_Repository = new Categorie_Repository(_pizzerieContext);
                }

                return _categorie_Repository;
            }

        }

        public RepositoryWrapper(PizzerieContext pizzerieContext)
        {
            _pizzerieContext = pizzerieContext;
        }

        public void Save()
        {
            _pizzerieContext.SaveChanges();
        }
    }
}