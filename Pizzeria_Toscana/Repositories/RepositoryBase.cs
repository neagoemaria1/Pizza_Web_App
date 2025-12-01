
using System.Linq.Expressions;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Models;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria_Toscana.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PizzerieContext PizzerieContext { get; set; }

        public RepositoryBase(PizzerieContext pizzerieContext)
        {
            this.PizzerieContext = pizzerieContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.PizzerieContext.Set<T>().AsNoTracking();
        }
         
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.PizzerieContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.PizzerieContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.PizzerieContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.PizzerieContext.Set<T>().Remove(entity);
        }
    }
}