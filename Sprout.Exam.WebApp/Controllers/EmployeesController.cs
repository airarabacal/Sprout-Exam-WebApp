using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business;
using Sprout.Exam.Business.Creators;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IBaseRepository<EmployeeDto> _employeeRepository;

        private readonly IUnitOfWork _unitOfwork;


        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
            _employeeRepository = _unitOfwork.GetRepository<EmployeeDto>();
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
            //return Ok(await employeeRepository.GetEmployeesAsync());
            return Ok(await _employeeRepository.GetAsync());
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _employeeRepository.GetByIDAsync(id));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(EmployeeDto input)
        {
            try
            {
                _employeeRepository.Update(input);
                _unitOfwork.Commit();
            }
            catch(Exception ex)
            {
                throw new BadHttpRequestException("error in updating", ex);
            }
            return Ok(input);
        }
        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto input)
        {
            try
            {
                await _employeeRepository.AddAsync(input);
                _unitOfwork.Commit();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("error in inserting", ex);
            }
            return Ok(input);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeRepository.Delete(id);
                _unitOfwork.Commit();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("error in deleting", ex);
            }
            return Ok(true);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        public IActionResult Calculate(CalculateSalaryDto input)
        {
            return Ok(CalculateSalaryEmployee(input));

        }

        public double CalculateSalaryEmployee(CalculateSalaryDto input)
        {

            if (input == null) return 0;

            SalaryFactory factory = new ConcreteSalaryFactory();
            var salaryCalculator = factory.GetSalaryFactory(input);
            var result = salaryCalculator.ComputeSalary();
            return result;
        }

    }
}
