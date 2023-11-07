using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Interfaces;
using System.Threading.Tasks;
using UnitTest.MockData;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateEmployee_Via_UnitOfWork_and_repository()
        {
            var mockSet = new Mock<DbSet<EmployeeDto>>();
            var mockOptions = Mock.Of<DbContextOptions>();
            var mockOperationalStoreOptions = Mock.Of<IOptions<OperationalStoreOptions>>();
            var mockContext = new Mock<FakeDbContext>(mockOptions, mockOperationalStoreOptions);
            //mockContext.Setup(c => c.Employee).Returns(mockSet.Object);
            using (var uof = new UnitOfWork(mockContext.Object))
            {
                IBaseRepository<EmployeeDto> _employeeRepository = uof.GetRepository<EmployeeDto>();
                var updateEmployee = new EmployeeDto();
                updateEmployee.Id = 28;
                updateEmployee.FullName = "unit test";
                updateEmployee.Birthdate = new System.DateTime(2000, 1, 1);
                updateEmployee.Tin = "123";
                updateEmployee.EmployeeTypeId = 2;

                Task.Run(async () => { await _employeeRepository.AddAsync(updateEmployee); }) ;

                uof.Commit();
            }
            mockSet.Verify(m => m.Add(It.IsAny<EmployeeDto>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            //var count = await mockContext.Object.Employee.CountAsync<EmployeeDto>();
            //Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void IsTesting()
        {
            //await Task.Delay(100);
            Assert.IsTrue(0 == 0);
        }
    }
}
