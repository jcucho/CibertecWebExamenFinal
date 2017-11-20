using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("api/Playlist")]
    public class PlaylistController : BaseController
    {
        public PlaylistController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.Playlists.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.Playlists.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Playlist playlist)
        {
            if (ModelState.IsValid)
                return Ok(_unit.Playlists.Insert(playlist));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Playlist playlist)
        {
            if (ModelState.IsValid && _unit.Playlists.Update(playlist))
                return Ok(new { Message = "The customer is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var playlist = _unit.Playlists.GetById(id);
            if (playlist.PlaylistId > 0)
                return Ok(_unit.Playlists.Delete(playlist));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Playlists.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Playlists.PagedList(startRecord, endRecord));
        }
    }
}