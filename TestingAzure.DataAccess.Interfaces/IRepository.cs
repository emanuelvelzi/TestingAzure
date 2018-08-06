using System.Linq;

namespace TestingAzure.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Create(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
