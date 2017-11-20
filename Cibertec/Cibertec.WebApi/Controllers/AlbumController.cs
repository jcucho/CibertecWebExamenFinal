using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{    
    [Route("api/album")]
    public class AlbumController : BaseController
    {
        public AlbumController(IUnitOfWork unit) : base(unit)
        {                
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.Albums.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.Albums.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Album album)
        {
            if (ModelState.IsValid)
                return Ok(_unit.Albums.Insert(album));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Album album)
        {
            if (ModelState.IsValid && _unit.Albums.Update(album))
                return Ok(new { Message = "The album is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var album = _unit.Albums.GetById(id);
            if (album.AlbumId > 0)
                return Ok(_unit.Albums.Delete(album));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Albums.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Albums.PagedList(startRecord, endRecord));
        }
    }
}