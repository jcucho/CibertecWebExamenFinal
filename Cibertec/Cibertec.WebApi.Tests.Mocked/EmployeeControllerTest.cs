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
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _employeeController;
        private readonly IUnitOfWork _uniMocked;

        public EmployeeControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _employeeController = new EmployeeController(_uniMocked);
        }

        [Fact(DisplayName = "[EmployeeController] Get List")]
        public void Get_All_Test()
        {
            var result = _employeeController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Employee>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[EmployeeController] Insert")]
        public void Insert_Artist_Test()
        {
            var employee = new Employee
            {
                EmployeeId = 9,
                FirstName = "Juan",
                LastName = "Test"
            };

            var result = _employeeController.Post(employee) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(9);
        }

        [Fact(DisplayName = "[EmployeeController] Update")]
        public void Update_Artist_Test()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Juan",
                LastName = "Test"
            };

            var result = _employeeController.Put(employee) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The Employee is updated");

            var currentEmployee = _uniMocked.Employees.GetById(1);
            currentEmployee.Should().NotBeNull();
            currentEmployee.FirstName.Should().Be(employee.FirstName);
            currentEmployee.LastName.Should().Be(employee.LastName);
        }

        [Fact(DisplayName = "[EmployeeController] Delete")]
        public void Delete_Artist_Test()
        {
            var employee = new Employee
            {
                EmployeeId = 1
            };

            var result = _employeeController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentEmployee = _uniMocked.Employees.GetById(1);
            currentEmployee.Should().BeNull();
        }
    }
}

