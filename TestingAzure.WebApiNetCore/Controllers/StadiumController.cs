using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.Entities;

namespace TestingAzure.WebApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Stadium")]
    public class StadiumController : Controller
    {
        private IUnitOfWork unitOfWork;

        public StadiumController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Stadium
        [HttpGet]
        public IEnumerable<Stadium> GetStadium()
        {
            return this.unitOfWork.GetRepository<Stadium>().GetAll();
        }

        // GET: api/Stadium/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStadium([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Stadium stadium = this.unitOfWork.GetRepository<Stadium>().GetById(id);

            if (stadium == null)
            {
                return NotFound();
            }

            return Ok(stadium);
        }

        // PUT: api/Stadium/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStadium([FromRoute] int id, [FromBody] Stadium stadium)
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

            return Ok(stadium);
        }

        // POST: api/Stadium
        [HttpPost]
        public async Task<IActionResult> PostStadium([FromBody] Stadium stadium)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStadium([FromRoute] int id)
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
    }
}