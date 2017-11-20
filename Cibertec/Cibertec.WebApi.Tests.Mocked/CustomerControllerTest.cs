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
    public class CustomerControllerTest
    {
        private readonly CustomerController _customerController;
        private readonly IUnitOfWork _uniMocked;

        public CustomerControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _customerController = new CustomerController(_uniMocked);            
        }

        [Fact(DisplayName = "[CustomerController] Get List")]
        public void Get_All_Test()
        {
            var result = _customerController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Customer>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[CustomerController] Insert")]
        public void Insert_Customer_Test()
        {
            var customer = new Customer
            {
                CustomerId = 60,
                FirstName = "Juan",
                LastName = "Cucho",
                Email = "juan.cucho@outlook.com",
                SupportRepId = 3
            };
            var result = _customerController.Post(customer) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(60);
        }

        [Fact(DisplayName = "[CustomerController] Update")]
        public void Update_Customer_Test()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Juan",
                LastName = "Cucho",
                Email = "juan.cucho@outlook.com",
                SupportRepId = 3
            };

            var result = _customerController.Put(customer) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The customer is updated");

            var currentCustomer = _uniMocked.Customers.GetById(1);
            currentCustomer.Should().NotBeNull();
            currentCustomer.CustomerId.Should().Be(customer.CustomerId);
            currentCustomer.FirstName.Should().Be(customer.FirstName);
            currentCustomer.LastName.Should().Be(customer.LastName);
            currentCustomer.SupportRepId.Should().Be(customer.SupportRepId);
        }

        [Fact(DisplayName = "[CustomerController] Delete")]
        public void Delete_Customer_Test()
        {
            var customer = new Customer
            {
                CustomerId = 1
            };
            var result = _customerController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentCustomer = _uniMocked.Customers.GetById(1);
            currentCustomer.Should().BeNull();
        }
    }
}
