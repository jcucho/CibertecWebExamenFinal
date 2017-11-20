using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;


namespace Cibertec.WebApi.Controllers
{
    [Route("api/invoiceLine")]
    public class InvoiceLineController : BaseController
    {
        public InvoiceLineController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.InvoiceLines.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.InvoiceLines.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
                return Ok(_unit.InvoiceLines.Insert(invoiceLine));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid && _unit.InvoiceLines.Update(invoiceLine))
                return Ok(new { Message = "The Invoice Line is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var invoiceLine = _unit.InvoiceLines.GetById(id);
            if (invoiceLine.InvoiceLineId > 0)
                return Ok(_unit.InvoiceLines.Delete(invoiceLine));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.InvoiceLines.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.InvoiceLines.PagedList(startRecord, endRecord));
        }
    }
}