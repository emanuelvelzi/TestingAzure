using NHibernate;
using System.Linq;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.NHibernate
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ISession Session { get; set; }

        public Repository(ISession session)
        {
            this.Session = session;
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public void Create(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(object id)
        {
            Session.Delete(Session.Load<T>(id));
        }
    }
}
