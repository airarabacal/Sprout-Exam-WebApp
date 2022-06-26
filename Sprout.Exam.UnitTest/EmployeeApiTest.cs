using Microsoft.Extensions.Configuration;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class EmployeeApiTest
    {
        private readonly IConfiguration _configuration;
        private int _newId = 0;
        public EmployeeApiTest()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.test.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }
        [Fact]
        public async void GetAllEmployees()
        {
            var controller = new EmployeesController(_configuration);
            var results = await controller.GetAllEmployees() as List<EmployeeDto>;
            Assert.Equal(8, results.Count); //get total count in database and change expected result
        }

        [Fact]
        public async void GetEmployeeById()
        {
            var controller = new EmployeesController(_configuration);
            var results = await controller.GetEmployeeById(28) as EmployeeDto;
            Assert.Equal("Jane Doe test", results.FullName);
        }

        [Fact]
        public async void UpdateEmployee()
        {
            var controller = new EmployeesController(_configuration);

            var updateEmployee = new EmployeeDto();
            updateEmployee.Id = 28;
            updateEmployee.FullName = "Jane Doe test";
            updateEmployee.Birthdate = "2022-06-09";
            updateEmployee.Tin = "123";
            updateEmployee.TypeId = 2;
            var results = await controller.UpdateEmployee(updateEmployee) as EmployeeDto;
            //Assert.Equal<EmployeeDto>(updateEmployee, results);
            Assert.Equal(updateEmployee.Id, results.Id);
            Assert.Equal(updateEmployee.FullName, results.FullName);
            Assert.Equal(updateEmployee.Birthdate, results.Birthdate);
            Assert.Equal(updateEmployee.Tin, results.Tin);
            Assert.Equal(updateEmployee.TypeId, results.TypeId);
        }

        [Fact]
        public async void AddEmployee()
        {
            var controller = new EmployeesController(_configuration);

            var newEmployee = new EmployeeDto();
            newEmployee.Id = 0;
            newEmployee.FullName = "unit test";
            newEmployee.Birthdate = "2022-06-09";
            newEmployee.Tin = "123";
            newEmployee.TypeId = 1;
            var results = await controller.AddEmployee(newEmployee) as EmployeeDto;
            Assert.Equal(newEmployee.FullName, results.FullName);
            Assert.Equal(newEmployee.Birthdate, results.Birthdate);
            Assert.Equal(newEmployee.Tin, results.Tin);
            Assert.Equal(newEmployee.TypeId, results.TypeId);
            _newId = newEmployee.Id;
        }

        [Fact]
        public async void Delete()
        {
            var controller = new EmployeesController(_configuration);

            var results = await controller.DeleteEmployee(_newId);
            Assert.True(results);
        }

        [Fact]
        public async void CalculateRegular()
        {
            var controller = new EmployeesController(_configuration);

            var salaryInfo = new CalculateSalaryDto();
            salaryInfo.Id = 34;
            salaryInfo.Days = 1;
            var results = await controller.CalculateSalaryEmployee(salaryInfo);
            Assert.Equal(16690.91, results);
        }
        [Fact]
        public async void CalculateContractual()
        {
            var controller = new EmployeesController(_configuration);

            var salaryInfo = new CalculateSalaryDto();
            salaryInfo.Id = 28;
            salaryInfo.Days = 15.5;
            var results = await controller.CalculateSalaryEmployee(salaryInfo);
            Assert.Equal(7750.00, results);
        }
    }
}
