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
    public class InvoiceControllerTest
    {
        private readonly InvoiceController _invoiceController;
        private readonly IUnitOfWork _uniMocked;

        public InvoiceControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _invoiceController = new InvoiceController(_uniMocked);
        }

        [Fact(DisplayName = "[InvoiceController] Get List")]
        public void Get_All_Test()
        {
            var result = _invoiceController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Invoice>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[InvoiceController] Insert")]
        public void Insert_Invoice_Test()
        {
            var invoice = new Invoice
            {
                InvoiceId = 413,
                CustomerId = 58,
                InvoiceDate = DateTime.Now,
                Total = 1
            };

            var result = _invoiceController.Post(invoice) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(413);
        }

        [Fact(DisplayName = "[InvoiceController] Update")]
        public void Update_Invoice_Test()
        {
            var invoice = new Invoice
            {
                InvoiceId = 1,
                CustomerId = 2,
                InvoiceDate = DateTime.Now,
                Total = 1
            };

            var result = _invoiceController.Put(invoice) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The invoice is updated");

            var currentInvoice = _uniMocked.Invoices.GetById(1);
            currentInvoice.Should().NotBeNull();
            currentInvoice.CustomerId.Should().Be(invoice.CustomerId);
            currentInvoice.InvoiceDate.Should().Be(invoice.InvoiceDate);
            currentInvoice.Total.Should().Be(invoice.Total);
        }

        [Fact(DisplayName = "[InvoiceController] Delete")]
        public void Delete_Invoice_Test()
        {
            var invoice = new Invoice
            {
                InvoiceId = 1
            };

            var result = _invoiceController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentInvoice = _uniMocked.Invoices.GetById(1);
            currentInvoice.Should().BeNull();
        }
    }
}
