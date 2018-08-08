using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.Entities;

namespace TestingAzure.WebApi.Controllers
{
    public class StadiumController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public StadiumController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Stadium
        public IQueryable<Stadium> GetStadium()
        {
            return this.unitOfWork.GetRepository<Stadium>().GetAll();
        }

        // GET: api/Stadium/5
        [ResponseType(typeof(Stadium))]
        public IHttpActionResult GetStadium(int id)
        {
            Stadium stadium = this.unitOfWork.GetRepository<Stadium>().GetById(id);
            if (stadium == null)
            {
                return NotFound();
            }

            return Ok(stadium);
        }

        // PUT: api/Stadium/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStadium(int id, Stadium stadium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stadium.Id)
            {
                return BadRequest();
            }

            this.unitOfWork.BeginTransaction();
            IRepository<Stadium> repo = this.unitOfWork.GetRepository<Stadium>();
            repo.Update(stadium);
            this.unitOfWork.Commit();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stadium
        [ResponseType(typeof(Stadium))]
        public IHttpActionResult PostStadium(Stadium stadium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.unitOfWork.BeginTransaction();
            IRepository<Stadium> repo = this.unitOfWork.GetRepository<Stadium>();
            repo.Create(stadium);
            this.unitOfWork.Commit();

            return Created("Stadiums", stadium);
        }

        // DELETE: api/Stadium/5
        [ResponseType(typeof(Stadium))]
        public IHttpActionResult DeleteStadium(int id)
        {
            Stadium stadium = this.unitOfWork.GetRepository<Stadium>().GetById(id);
            if (stadium == null)
            {
                return NotFound();
            }

            this.unitOfWork.BeginTransaction();
            IRepository<Stadium> repo = this.unitOfWork.GetRepository<Stadium>();
            repo.Delete(stadium.Id);
            this.unitOfWork.Commit();


            return Ok(stadium);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}