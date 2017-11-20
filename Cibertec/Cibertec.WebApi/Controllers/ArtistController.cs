using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("api/artist")]
    public class ArtistController : BaseController
    {
        public ArtistController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.Artists.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.Artists.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Artist artist)
        {
            if (ModelState.IsValid)
                return Ok(_unit.Artists.Insert(artist));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Artist artist)
        {
            if (ModelState.IsValid && _unit.Artists.Update(artist))
                return Ok(new { Message = "The artist is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var artist = _unit.Artists.GetById(id);
            if (artist.ArtistId > 0)
                return Ok(_unit.Artists.Delete(artist));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Artists.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Artists.PagedList(startRecord, endRecord));
        }
    }
}