using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.DataTransferObjects
{
    public class EmployeeDto
    {

        public EmployeeDto(int id, string fullName, DateTime birthdate, string tin, int employeeTypeId, bool isDeleted)
        {
            Id = id;
            FullName = fullName;
            Birthdate = birthdate;
            Tin = tin;
            EmployeeTypeId = employeeTypeId;
            IsDeleted = isDeleted;
        }

        public EmployeeDto()
        {

        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
