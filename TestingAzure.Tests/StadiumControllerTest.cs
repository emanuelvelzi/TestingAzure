using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.Entities;
using TestingAzure.WebApi.Controllers;

namespace TestingAzure.Tests
{
    public class StadiumControllerTest
    {
        private List<Stadium> StadiumsForTests;
        private StadiumController StadiumController;

        [SetUp]
        public void Init()
        {
            this.StadiumsForTests = new List<Stadium> {
                new Stadium {
                    Id = 1,
                    Capacity = 10000,
                    City = "La Plata",
                    Country = "Argentina" ,
                    Description = "",
                    Name = "Unico"
                }
            };
            IRepository<Stadium> repo = new RepositoryStadiumMock(this.StadiumsForTests);
            IUnitOfWork uow = new UnitOfWorkMock(repo);

            this.StadiumController = new StadiumController(uow);
        }

        [Test]
        public void GetStadium()
        {
            var stadiumsFromController = this.StadiumController.GetStadium();

            Assert.AreEqual(stadiumsFromController, this.StadiumsForTests);
        }
    }
}
