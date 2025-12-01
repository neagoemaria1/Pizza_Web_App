namespace Pizzeria_Toscana.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProdusRepository ProdusRepository { get; }
        IComanda_ProdusRepository Comanda_ProdusRepository { get; }
        IComandaRepository ComandaRepository { get; }
        IUserRepository UserRepository { get; }
        IProdus_IngredientRepository Produs_IngredientRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        ICosRepository CosRepository { get; }
        ICos_ProdusRepository Cos_ProdusRepository { get; }
        ICategorie_Repository Categorie_Repository { get; }
       
        void Save();
    }
} 