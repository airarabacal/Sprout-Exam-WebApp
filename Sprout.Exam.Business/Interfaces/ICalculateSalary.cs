using Sprout.Exam.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface ICalculateSalary
    {
        CalculateSalaryDto CalculateSalaryInfo { get; set; }
        double ComputeSalary();
    }
}
