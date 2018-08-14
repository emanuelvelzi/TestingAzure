using System.Collections.Generic;
using System.Linq;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.Entities;

namespace TestingAzure.Tests
{
    public class RepositoryStadiumMock : IRepository<Stadium>
    {
        private List<Stadium> Stadiums;
        public RepositoryStadiumMock(List<Stadium> stadiums)
        {
            this.Stadiums = stadiums;
        }

        public void Create(Stadium entity)
        {
            return;
        }

        public void Delete(object id)
        {
            return;
        }

        public IQueryable<Stadium> GetAll()
        {
            return this.Stadiums.AsQueryable();
        }

        public Stadium GetById(object id)
        {
            return this.Stadiums.Find(x => (object)x.Id == id);
        }

        public void Update(Stadium entity)
        {
            return;
        }
    }
}
