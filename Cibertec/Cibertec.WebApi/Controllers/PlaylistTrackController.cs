using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;


namespace Cibertec.WebApi.Controllers
{
    [Route("api/PlaylistTrack")]
    public class PlaylistTrackController : BaseController
    {
        public PlaylistTrackController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.PlaylistTracks.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.PlaylistTracks.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlaylistTrack playlistTrack)
        {
            if (ModelState.IsValid)
                return Ok(_unit.PlaylistTracks.Insert(playlistTrack));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PlaylistTrack playlistTrack)
        {
            if (ModelState.IsValid && _unit.PlaylistTracks.Update(playlistTrack))
                return Ok(new { Message = "The play list track is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var playlistTrack = _unit.PlaylistTracks.GetById(id);
            if (playlistTrack.PlaylistId > 0)
                return Ok(_unit.PlaylistTracks.Delete(playlistTrack));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.PlaylistTracks.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.PlaylistTracks.PagedList(startRecord, endRecord));
        }
    }
}