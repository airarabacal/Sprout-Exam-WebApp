using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Data
{
    public class EmployeeRepository: IEmployeeRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task <IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await Task.Run(() => { return _context.Employee.AsQueryable(); });
        }

        public virtual async Task<EmployeeDto> GetEmployeeByIDAsync(int id)
        {
            return await _context.Set<EmployeeDto>().FindAsync(id);
        }

        public virtual async Task AddEmployeeAsync(EmployeeDto employee)
        {
            await _context.Employee.AddAsync(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            EmployeeDto employee = _context.Employee.Find(employeeId);
            _context.Employee.Remove(employee);
        }

        public void UpdateEmployee(EmployeeDto employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
