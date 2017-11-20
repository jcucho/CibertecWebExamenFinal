using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("api/genre")]
    public class GenreController : BaseController
    {
        public GenreController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.Genres.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.Genres.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Genre genre)
        {
            if (ModelState.IsValid)
                return Ok(_unit.Genres.Insert(genre));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Genre genre)
        {
            if (ModelState.IsValid && _unit.Genres.Update(genre))
                return Ok(new { Message = "The genres is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var genre = _unit.Genres.GetById(id);
            if (genre.GenreId > 0)
                return Ok(_unit.Genres.Delete(genre));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Genres.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Genres.PagedList(startRecord, endRecord));
        }
    }
}