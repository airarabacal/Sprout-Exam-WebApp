using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Sprout.Exam.Business.DataAccessLayer;
using Sprout.Exam.WebApp.Classes;
using Sprout.Exam.WebApp.Classes.Creators;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly string _connectionString;

        private Transfer _transfer;
        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _transfer = new Transfer(_connectionString);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList);
            //var employees = new List<EmployeeDto>();
            return Ok(await GetAllEmployees());
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return await _transfer.GetAsync<EmployeeDto>("api_GetEmployees");
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await GetEmployeeById(id));
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            return (await _transfer.GetAsync<EmployeeDto>("api_GetEmployeeById", id)).FirstOrDefault();
        }
        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDto input)
        {
            return Ok(await UpdateEmployee(input));
        }
        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto input)
        {
            var updatedEmployee = await _transfer.PostAsync<EmployeeDto>("api_UpdateEmployee", input);
            return updatedEmployee;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto input)
        {
            return Ok(await AddEmployee (input));
        }
        public async Task<EmployeeDto> AddEmployee(EmployeeDto input)
        {
            var newEmployee = await _transfer.PostAsync<EmployeeDto>("api_SaveEmployee", input);
            return newEmployee;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await DeleteEmployee (id));
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                await _transfer.DeleteAsync("api_DeleteEmployee", id);
                return true;
            }
            catch(SystemException ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(CalculateSalaryDto input)
        {
            return Ok(await CalculateSalaryEmployee (input));

        }

        public async Task<double> CalculateSalaryEmployee(CalculateSalaryDto input)
        {
            var employees = (await _transfer.GetAsync<EmployeeDto>("api_GetEmployeeById", input.Id)).FirstOrDefault();

            if (employees == null) return 0;
            var type = (EmployeeType)employees.TypeId;

            CalculateSalaryCreator creator = Utils.GetCreator(type);
            var result = creator.CalculateSalary(Utils.GetRate(type), input.Days);
            return result;
        }

    }
}
