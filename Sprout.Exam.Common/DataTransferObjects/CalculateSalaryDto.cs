using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.DataTransferObjects
{
    public class CalculateSalaryDto
    {
        public int Id { get; set; }

        public int EmployeeTypeId { get; set; }
        public double AbsentDays { get; set; }
        public double WorkDays { get; set; }
    }
}
