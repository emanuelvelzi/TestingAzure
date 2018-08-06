using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> dbSet;

        public Repository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        public void Create(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            T entity = this.GetById(id);
            this.dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return this.dbSet.AsQueryable<T>();
        }

        public T GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
