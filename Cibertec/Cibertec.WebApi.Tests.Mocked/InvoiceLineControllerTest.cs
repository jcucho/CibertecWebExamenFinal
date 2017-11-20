using Cibertec.Mocked;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using Cibertec.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cibertec.WebApi.Tests.Mocked
{
    public class InvoiceLineControllerTest
    {
        private readonly InvoiceLineController _invoiceLineController;
        private readonly IUnitOfWork _uniMocked;

        public InvoiceLineControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _invoiceLineController = new InvoiceLineController(_uniMocked);
        }

        [Fact(DisplayName = "[InvoiceLineController] Get List")]
        public void Get_All_Test()
        {
            var result = _invoiceLineController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<InvoiceLine>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[InvoiceLineController] Insert")]
        public void Insert_InvoiceLine_Test()
        {
            var InvoiceLine = new InvoiceLine
            {
                InvoiceLineId = 2241,
                InvoiceId = 1,
                TrackId = 2,
                UnitPrice = 1
            };

            var result = _invoiceLineController.Post(InvoiceLine) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(2241);
        }

        [Fact(DisplayName = "[InvoiceLineController] Update")]
        public void Update_InvoiceLine_Test()
        {
            var InvoiceLine = new InvoiceLine
            {
                InvoiceLineId = 1,
                InvoiceId = 1,
                TrackId = 2,
                UnitPrice = 1
            };

            var result = _invoiceLineController.Put(InvoiceLine) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The Invoice Line is updated");

            var currentInvoiceLine = _uniMocked.InvoiceLines.GetById(1);
            currentInvoiceLine.Should().NotBeNull();
            currentInvoiceLine.InvoiceId.Should().Be(InvoiceLine.InvoiceId);
            currentInvoiceLine.TrackId.Should().Be(InvoiceLine.TrackId);
            currentInvoiceLine.UnitPrice.Should().Be(InvoiceLine.UnitPrice);
        }

        [Fact(DisplayName = "[InvoiceLineController] Delete")]
        public void Delete_InvoiceLine_Test()
        {
            var InvoiceLine = new InvoiceLine
            {
                InvoiceLineId = 1
            };

            var result = _invoiceLineController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentInvoiceLine = _uniMocked.InvoiceLines.GetById(1);
            currentInvoiceLine.Should().BeNull();
        }
    }
}
