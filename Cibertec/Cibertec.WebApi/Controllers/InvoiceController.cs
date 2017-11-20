using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("api/invoice")]
    public class InvoiceController : BaseController
    {
        public InvoiceController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unit.Invoices.GetList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unit.Invoices.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            if (ModelState.IsValid)
                return Ok(_unit.Invoices.Insert(invoice));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Invoice invoice)
        {
            if (ModelState.IsValid && _unit.Invoices.Update(invoice))
                return Ok(new { Message = "The invoice is updated" });
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var invoice = _unit.Invoices.GetById(id);
            if (invoice.InvoiceId > 0)
                return Ok(_unit.Invoices.Delete(invoice));
            return BadRequest(new { Message = "Incorrect data." });
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Invoices.Count());
        }

        [HttpGet]
        [Route("list/{page}/{rows}")]
        public IActionResult GetList(int page, int rows)
        {
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Invoices.PagedList(startRecord, endRecord));
        }
    }
}