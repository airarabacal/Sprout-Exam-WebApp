using Sprout.Exam.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IEmployeeRepository : IDisposable
    {
        Task <IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task <EmployeeDto> GetEmployeeByIDAsync(int employeeId);
        Task AddEmployeeAsync(EmployeeDto employee);
        void DeleteEmployee(int employeeId);
        void UpdateEmployee(EmployeeDto employee);
        void Save();

    }
}
