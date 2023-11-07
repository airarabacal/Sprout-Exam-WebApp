using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Business;
using Sprout.Exam.Business.Creators;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.xUnitTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.xUnitTest
{
    public class EmployeeApiTest
    {

        public EmployeeApiTest()
        {

        }

        [Fact]
        public async void CreateEmployee_Via_UnitOfWork_and_repository()
        {
            var fakeDbContext = CreateFakeDbContext();
            using (var uof = new UnitOfWork(fakeDbContext))
            {
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                var updateEmployee = new EmployeeDto();
                updateEmployee.Id = 28;
                updateEmployee.FullName = "unit test";
                updateEmployee.Birthdate = new System.DateTime(2000, 1, 1);
                updateEmployee.Tin = "123";
                updateEmployee.EmployeeTypeId = 2;

                await _employeeRepository.AddAsync(updateEmployee);
                uof.Commit();
                Assert.Equal(1, await fakeDbContext.Employee.CountAsync<EmployeeDto>());
            }
            fakeDbContext.Dispose();
        }

        [Fact]
        public async void GetAllEmployee_via_UnitOfWork_and_repository()
        {
            var fakeDbContext = CreateFakeDbContext();

            var employees = new EmployeeDto[3] {
                    new EmployeeDto(1, "aa", new System.DateTime(2000, 1, 1), "11", 2, false),
                    new EmployeeDto(2, "bb", new System.DateTime(2000, 1, 1), "22", 2, false),
                    new EmployeeDto(3, "cc", new System.DateTime(2000, 1, 1), "33", 2, false)
                };

            fakeDbContext.AddRange(employees);
            fakeDbContext.SaveChanges();
            using (var uof = new UnitOfWork(fakeDbContext))
            {
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                IEnumerable<EmployeeDto> retrievedEmployees = await _employeeRepository.GetAsync();
                Assert.Equal(3, Enumerable.Count<EmployeeDto>(retrievedEmployees));
            }
            fakeDbContext.Dispose();


        }

        [Fact]
        public async void Update_via_UnitOfWork_and_repository()
        {
            var fakeDbContext = CreateFakeDbContext();

            using (var uof = new UnitOfWork(fakeDbContext))
            {
                var updateEmployee = new EmployeeDto();
                updateEmployee.Id = 1;
                updateEmployee.FullName = "aa";
                updateEmployee.Birthdate = new System.DateTime(2000, 1, 1);
                updateEmployee.Tin = "11";
                updateEmployee.EmployeeTypeId = 1;

                fakeDbContext.Entry(updateEmployee).State = EntityState.Detached;
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                //await _employeeRepository.GetAsync();
                await _employeeRepository.AddAsync(updateEmployee);

                uof.Commit();

                updateEmployee.Id = 1;
                updateEmployee.FullName = "aa";
                updateEmployee.Birthdate = new System.DateTime(2000, 1, 1);
                updateEmployee.Tin = "44";
                updateEmployee.EmployeeTypeId = 2;
                _employeeRepository.Update(updateEmployee);

                var employeeFrDbContext = fakeDbContext.Employee.Where(e => e.Id == 1).FirstOrDefault();
                Assert.Equal(updateEmployee.Tin, employeeFrDbContext.Tin);
                Assert.Equal(updateEmployee.EmployeeTypeId, employeeFrDbContext.EmployeeTypeId);
            }
            fakeDbContext.Dispose();
        }

        [Fact]
        public void Delete_via_UnitOfWork_and_repository()
        {
            var fakeDbContext = CreateFakeDbContext();

            var employees = new EmployeeDto[3] {
                new EmployeeDto(1, "aa", new System.DateTime(2000, 1, 1), "11", 2, false),
                new EmployeeDto(2, "bb", new System.DateTime(2000, 1, 1), "22", 2, false),
                new EmployeeDto(3, "cc", new System.DateTime(2000, 1, 1), "33", 2, false)
            };

            fakeDbContext.AddRange(employees);
            fakeDbContext.SaveChanges();

            using (var uof = new UnitOfWork(fakeDbContext))
            {
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                _employeeRepository.Delete(3);

                uof.Commit();

                Assert.Equal(2, fakeDbContext.Employee.Count());
            }
            fakeDbContext.Dispose();
        }

        [Fact]
        public async void GetEmployee_via_UnitOfWork_and_repository()
        {
            var fakeDbContext = CreateFakeDbContext();

            var employees = new EmployeeDto[3] {
                new EmployeeDto(1, "aa", new System.DateTime(2000, 1, 1), "11", 2, false),
                new EmployeeDto(2, "bb", new System.DateTime(2000, 1, 1), "22", 2, false),
                new EmployeeDto(3, "cc", new System.DateTime(2006, 1, 1), "33", 1, false)
            };

            fakeDbContext.AddRange(employees);
            fakeDbContext.SaveChanges();

            using (var uof = new UnitOfWork(fakeDbContext))
            {
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                var employee = await _employeeRepository.GetByIDAsync(3);


                Assert.Equal("cc", employee.FullName);
                Assert.Equal(new System.DateTime(2006, 1, 1), employee.Birthdate);
                Assert.Equal("33", employee.Tin);
                Assert.Equal(1, employee.EmployeeTypeId);

            }
            fakeDbContext.Dispose();
        }

        [Fact]
        public void Check_If_Gets_Correct_RegularSalary_Method()
        {
            var calcInfo = new CalculateSalaryDto();
            calcInfo.Id = 1;
            calcInfo.AbsentDays = 34;
            calcInfo.EmployeeTypeId = 1;
            SalaryFactory factory = new ConcreteSalaryFactory();
            var salaryCalculator = factory.GetSalaryFactory(calcInfo);

            Assert.IsType<RegularCalculateSalary>(salaryCalculator);
        }

        [Fact]
        public void Check_If_Gets_Correct_ContractualSalary_Method()
        {
            var calcInfo = new CalculateSalaryDto();
            calcInfo.Id = 1;
            calcInfo.WorkDays = 34;
            calcInfo.EmployeeTypeId = 2;
            SalaryFactory factory = new ConcreteSalaryFactory();
            var salaryCalculator = factory.GetSalaryFactory(calcInfo);

            Assert.IsType<ContractualCalculateSalary>(salaryCalculator);
        }

        [Fact]
        public void CalculateRegularSalary()
        {
            var calcInfo = new CalculateSalaryDto();
            calcInfo.Id = 1;
            calcInfo.AbsentDays = 1;
            calcInfo.EmployeeTypeId = 1;
            SalaryFactory factory = new ConcreteSalaryFactory();
            var salaryCalculator = factory.GetSalaryFactory(calcInfo);
            var result = salaryCalculator.ComputeSalary();
            Assert.Equal(16690.91, result);

        }

        [Fact]
        public void CalculateContractualSalary()
        {
            var calcInfo = new CalculateSalaryDto();
            calcInfo.Id = 1;
            calcInfo.WorkDays = 15.5;
            calcInfo.EmployeeTypeId = 2;
            SalaryFactory factory = new ConcreteSalaryFactory();
            var salaryCalculator = factory.GetSalaryFactory(calcInfo);
            var result = salaryCalculator.ComputeSalary();
            Assert.Equal(7750, result);

        }


        private FakeDbContext CreateFakeDbContext()
        {
            DbContextOptions<FakeDbContext> dbContextOptions = new DbContextOptionsBuilder<FakeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            OperationalStoreOptions storeOptions = new OperationalStoreOptions
            {
                //populate needed members
            };

            IOptions<OperationalStoreOptions> operationalStoreOptions = Options.Create(storeOptions);

            return new FakeDbContext(dbContextOptions, operationalStoreOptions);
        }
    }
}
